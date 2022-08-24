using BusinessLogic.Dtos.PageDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validations.FluentValidations.Page
{
    public class CreatePageDtoValidator: AbstractValidator<CreatePageDto>
    {
        public CreatePageDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(2).MaximumLength(100);

            RuleFor(x => x.Content).NotEmpty().MinimumLength(2).MaximumLength(500);
        }
    }
}
