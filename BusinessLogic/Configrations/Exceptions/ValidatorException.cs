using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Configrations.Exceptions
{
    public static class ValidatorException
    {
     
        public static void throwIfValidationException(this FluentValidation.Results.ValidationResult validationResult)
        {
            if (validationResult.IsValid)
                return;

            var messages = string.Join(',', validationResult.Errors.Select(x => x.PropertyName + " : " + x.ErrorMessage));
            throw new ValidationException(messages);
        }
    }
}
