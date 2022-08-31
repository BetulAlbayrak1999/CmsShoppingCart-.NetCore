﻿using BusinessLogic.Dtos.CartItemDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Validations.FluentValidations.CartItem
{
    public class CreateCartItemDtoValidator : AbstractValidator<CreateCartItemDto>
    {
        public CreateCartItemDtoValidator()
        {
        }
    }
    
}
