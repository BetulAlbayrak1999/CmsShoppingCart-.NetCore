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
    public class CartsController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IMapper _autoMapper;

        public CartsController(ICartItemService CartItemService, IMapper autoMapper)
        {
            _cartItemService = CartItemService;
            _autoMapper = autoMapper;
        }

        //GET/cart
        [Route("Carts")]
        public IActionResult Index()
        {
            try
            {
                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();

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

        // GET /carts/create/5
        
        public async Task<IActionResult> Create([FromRoute] int productId)
        {
            try
            {
                if (productId == 0)
                    return NotFound();

                var product = await _cartItemService.GetProductByIdAsync(productId);
                if (product is null)
                    return NotFound();

                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();
                
                var cartItem = await _cartItemService.GetCartItemByProductIdAsync(productId);
                if (cartItem is null)
                    cart.Add(new CartItemDto(product));
               
                else
                {
                    cartItem.Quantity = 1;
                }

                HttpContext.Session.SetJson("Cart", cart);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { return View(); }
        }
    }
}
