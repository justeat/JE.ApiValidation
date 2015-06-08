using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenRasta.DI;
using OpenRasta.DI.Internal;
using OpenRasta.Owin;
using OpenRasta.Pipeline;
using StructureMap;
using StructureMap.Pipeline;

namespace JE.ApiValidation.Examples.OpenRasta
{
    public class StructureMapDependencyResolver : DependencyResolverCore, IDependencyResolver
    {
        private readonly IContainer _container;
        private static readonly ReaderWriterLock ApplicationInstanceLock = new ReaderWriterLock();

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        static bool IsInHttpContext
        {
            get { return System.Web.HttpContext.Current != null; }
        }

        public bool HasDependency(Type serviceType)
        {
            return _container.TryGetInstance(serviceType) != null;
        }

        public bool HasDependencyImplementation(Type serviceType, Type concreteType)
        {
            return _container.Model.HasImplementationsFor(serviceType) && _container.Model.For(serviceType).Instances.Any(i => i.ReturnedType == concreteType);
        }

        public void HandleIncomingRequestProcessed()
        {
            var contextStore = _container.GetInstance<IContextStore>();
            contextStore.Destruct();

            if (!IsInHttpContext)
                return;

            var commContext = System.Web.HttpContext.Current.Items["__OR_COMM_CONTEXT"] as OwinCommunicationContext;
            if (commContext != null)
            {
                commContext.OperationResult = null;
                commContext.PipelineData = null;
            }
//
//            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
//            HttpContextLifecycle.DisposeAndClearAll();
        }

        protected override void AddDependencyCore(Type serviceType, Type concreteType, DependencyLifetime lifetime)
        {
            _container.Configure(
                cfg => cfg.For(serviceType).LifecycleIs(GetLifecycle(lifetime)).Use(concreteType));
        }

        private static ILifecycle GetLifecycle(DependencyLifetime lifetime)
        {
            switch (lifetime)
            {
                case DependencyLifetime.PerRequest:
                    return new UniquePerRequestLifecycle();
                case DependencyLifetime.Singleton:
                    return new SingletonLifecycle();
                case DependencyLifetime.Transient:
                    return new TransientLifecycle();
                default:
                    return new ThreadLocalStorageLifecycle();
            }
        }

        protected override void AddDependencyCore(Type concreteType, DependencyLifetime lifetime)
        {
            _container.Configure(cfg => cfg.For(concreteType)
                                            .LifecycleIs(GetLifecycle(lifetime))
                                            .Use(concreteType));
        }

        protected override void AddDependencyInstanceCore(Type serviceType, object instance, DependencyLifetime lifetime)
        {
            if (lifetime != DependencyLifetime.PerRequest || !IsInHttpContext)
            {
                _container.Configure(cfg => cfg.For(serviceType).LifecycleIs(GetLifecycle(lifetime)).Use(instance));
            }
            else
            {
                var typeKey = "__foo" + serviceType.FullName;

                ApplicationInstanceLock.AcquireReaderLock(Timeout.Infinite);
                try
                {
                    if (System.Web.HttpContext.Current.Application[typeKey] == null)
                    {
                        var lc = ApplicationInstanceLock.UpgradeToWriterLock(Timeout.Infinite);
                        try
                        {
                            _container.Configure(cfg => cfg.For(serviceType)
                                                            .LifecycleIs(GetLifecycle(DependencyLifetime.Transient))
                                                            .Use(GetInstanceFromContext(typeKey)));

                            System.Web.HttpContext.Current.Application[typeKey] = true;
                        }
                        finally
                        {
                            ApplicationInstanceLock.DowngradeFromWriterLock(ref lc);
                        }
                    }
                }
                finally
                {
                    ApplicationInstanceLock.ReleaseReaderLock();
                }
                System.Web.HttpContext.Current.Items[typeKey] = instance;
            }
        }

        private static Func<IContext, object> GetInstanceFromContext(string typeKey)
        {
            return x => System.Web.HttpContext.Current.Items[typeKey];
        }

        protected override IEnumerable<TService> ResolveAllCore<TService>()
        {
            return _container.GetAllInstances<TService>();
        }

        protected override object ResolveCore(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }
    }
}
