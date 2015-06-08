using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.ModelBinding;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.WebApi
{
    [DataContract]
    public class ResponseForInvalidRequest : StandardErrorResponse
    {
        public ResponseForInvalidRequest(ModelStateDictionary modelState)
            : base((int)ErrorCodes.RequestWasInvalid, "The request is invalid.")
        {
            SerializeFromModelState(modelState);
        }

        private void SerializeFromModelState(ModelStateDictionary modelState)
        {
            foreach (var keyModelStatePair in modelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors == null || errors.Count <= 0)
                {
                    continue;
                }

                IList<Problem> problems = errors.Select(error => string.IsNullOrWhiteSpace(error.ErrorMessage)
                                                                     ? new Problem { Message = "Undefined error." }
                                                                     : new Problem { Message = error.ErrorMessage }).ToList();
                Errors.Add(key, problems);
            }
        }
    }
}
