using FluentValidation;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.Shared
{
    public class ResponseForProcessingError : FluentValidationErrorResponse
    {
        public ResponseForProcessingError(ValidationException exception)
            : base((int)ErrorCodes.ErrorDuringProcessing, "Error during processing", exception.Errors)
        {
        }
    }
}
