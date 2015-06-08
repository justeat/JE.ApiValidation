using System;
using System.Collections.Generic;
using FluentValidation;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.Shared;
using OpenRasta.OperationModel;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Web;

namespace JE.ApiValidation.OpenRasta
{
    public class FluentValidationResponseProcessingOperationInterceptor : OperationInterceptor
    {
        public override Func<IEnumerable<OutputMember>> RewriteOperation(
            Func<IEnumerable<OutputMember>> operationBuilder)
        {
            return () => TryExecute(operationBuilder);
        }

        private IEnumerable<OutputMember> TryExecute(Func<IEnumerable<OutputMember>> operationBuilder)
        {
            try
            {
                return operationBuilder();
            }
            catch (Exception ex)
            {
                var validationException = ex.GetBaseException() as ValidationException;
                if (validationException != null)
                {
                    var responseBody = new ResponseForProcessingError(validationException);
                    LogBadRequest(responseBody);
                    return new[]
                               {
                                   new OutputMember
                                       {
                                           Value =
                                               new OperationResult.BadRequest
                                                   {
                                                       ResponseResource =
                                                           responseBody
                                                   }
                                       }
                               };
                }
                throw;
            }
        }

        protected virtual void LogBadRequest(StandardErrorResponse responseBody)
        {

        }
    }
}
