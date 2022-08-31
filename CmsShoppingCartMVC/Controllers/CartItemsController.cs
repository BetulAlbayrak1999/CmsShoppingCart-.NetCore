using AutoMapper;
using BusinessLogic.Configrations.Extensions;
using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.Services.CartItemServices;
using BusinessLogic.ViewModels.CartItemViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _autoMapper;

        public CartItemsController(ICartItemService CartItemService, IMapper autoMapper)
        {
            _cartItemService = CartItemService;
            _autoMapper = autoMapper;
        }

        //GET/cart
        [Route("CartItems")]
        public IActionResult Index()
        {
            try
            {
                List<GetCartItemDto> cart = HttpContext.Session.GetJson<List<GetCartItemDto>>("Cart") ?? new List<GetCartItemDto>();

                CartItemVM cartItemVM = new CartItemVM
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(x => x.Price * x.Quantity)
                };
                if(cartItemVM is not null)
                    return View(cartItemVM);
                return NotFound();

            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
