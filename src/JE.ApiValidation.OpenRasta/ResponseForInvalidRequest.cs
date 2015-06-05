using System.Collections.Generic;
using System.Runtime.Serialization;
using FluentValidation.Results;
using JE.ApiValidation.DTOs;
using JE.ApiValidation.Shared;

namespace JE.ApiValidation.OpenRasta
{
    [DataContract]
    public class ResponseForInvalidRequest : FluentValidationErrorResponse
    {
        public ResponseForInvalidRequest(IEnumerable<ValidationFailure> errors)
            : base((int)ErrorCodes.RequestWasInvalid, "The request is invalid.", errors)
        {
        }
    }
}
