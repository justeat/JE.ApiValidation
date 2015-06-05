using System.Collections.Generic;
using FluentValidation.Results;
using JE.ApiValidation.DTOs;

namespace JE.ApiValidation.Shared
{
    public abstract class FluentValidationErrorResponse : StandardErrorResponse
    {
        protected FluentValidationErrorResponse(
            int code = 0,
            string message = "",
            IEnumerable<ValidationFailure> errors = null)
            : base(code, message)
        {
            if (errors != null)
            {
                SerializeFromValidationFailures(errors);
            }
        }

        private void SerializeFromValidationFailures(IEnumerable<ValidationFailure> errors)
        {
            foreach (var e in errors)
            {
                if (!Errors.ContainsKey(e.PropertyName))
                {
                    Errors[e.PropertyName] = new List<Problem>();
                }

                var problem = new Problem
                                  {
                                      Message = e.ErrorMessage,
                                      AttemptedValue = e.AttemptedValue,
                                      CustomState = e.CustomState
                                  };
                Errors[e.PropertyName].Add(problem);
            }
        }
    }
}
