using FluentValidation;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.WebApi.FluentValidation
{
    public class ResponseForProcessingError : FluentValidationErrorResponse
    {
        public ResponseForProcessingError(ValidationException exception)
            : base((int)ErrorCodes.ErrorDuringProcessing, "Error during processing", exception.Errors)
        {
        }
    }
}