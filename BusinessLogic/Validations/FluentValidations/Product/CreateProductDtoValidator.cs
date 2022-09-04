using BusinessLogic.Dtos.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validations.FluentValidations.Product
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100);

            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.Description).NotEmpty().MinimumLength(4).MaximumLength(500);
        }
    }
}
