Registering and retrieving Validators via StructureMap
======================================================

When using FluentValidation you will likely have some classes along these lines:

```csharp
public class YourClass 
{
    public int Id {get; set;}
    public string Name {get; set;}
}

public class YourValidator : AbstractValidator<YourClass>()
{
    public YourValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name).NotNull().NotEmpty();
    }
}

public class YourController : ApiController
{
    public IHttpActionResult DoStuff(YourClass yourClass)
    {
        //do some stuff here with YourClass
    }
}
```


You can setup your WebApi/Mvc project to automatically run any validators you have against models passed into 
controller calls. So in the above example, when the `DoStuff` action is called, we want to make sure that 
`YourClass` has valid information. If the properties in `YourClass` are invalid, we should automatically return
a 404 message and the details fo the error.


How to to configure
-------------------

__1. Registering Validators in structuremap__

Where you setup you Structuremap configuration at startup, ensure that the following settings are included:

```csharp
var container = new Container(register =>
{
    register.Scan(s =>
    {
        s.TheCallingAssembly(); // or any of the other "assembly scan" type functions. eg: s.AssemblyContainingType<YourValidator>();
        s.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
    });
});
```

The above code snippet will scan the assembly(s) specified for all implementations of AbstractValidator and add
them to the strucutemap container.


__2. Adding the filter__

In your statup / global.asax file, you will have somewhere that you can register extra filters that should be run
on each call.

For a WebApi using Owin, the Startup.cs will look something like this:

```csharp
[assembly: OwinStartup(typeof(Startup))]
namespace MyWebApi
{
    public class Startup
    {
        private readonly IContainer _container;

        public Startup()
        {
            _container = CreateDefaultRegistry();
        }

        public void Configuration(IAppBuilder app)
        {
            app.UseWebApi(MyWebApiConfiguration());
        }

        // configure structuremap container so it knows about all implementations of AbstractValidator<>
        private IContainer CreateDefaultRegistry()
        {
            var container = new Container(register =>
            {
                register.Scan(s =>
                {
                    s.TheCallingAssembly(); // or any of the other "assembly scan" type functions. eg: s.AssemblyContainingType<YourValidator>();
                    s.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                });
            });

            return container;
        }

        private HttpConfiguration MyWebApiConfiguration()
        {
            var config = new HttpConfiguration
            {
                DependencyResolver = new DependencyResolver(_container)
            };

            ConfigureRequestValidation(config);

            return config;
        }

        // configure FluentValidation to use the StructureMapValidatorFactory to find the Validators
        private void ConfigureRequestValidation(HttpConfiguration config)
        {
            FluentValidationModelValidatorProvider.Configure(config, provider => provider.ValidatorFactory = new StructureMapValidatorFactory(_container));
        }
    }
}
```




Thats it, when making requests to the `DoStuff` action in `YourController`, it will ensure that `YourClass` 
has valid data. If it doesn't the request will not even enter into the `DoStuff` action. It will return a 404 
with the validation errors.

Example
-------

Check out the example project "JE.ApiValidation.Examples.WebApi.FluentValidation.StructureMap" for a working example