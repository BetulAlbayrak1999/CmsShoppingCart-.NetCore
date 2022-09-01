using AutoMapper;
using BusinessLogic.Configrations.Extensions;
using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.Services.CartServices;
using BusinessLogic.ViewModels.CartViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Controllers
{
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _autoMapper;

        public CartsController(ICartService CartService, IMapper autoMapper)
        {
            _cartService = CartService;
            _autoMapper = autoMapper;
        }

        //GET/cart
        [Route("Carts")]
        public IActionResult Index()
        {
            try
            {
                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();

                CartVM cartVM = new CartVM
                {
                    CartItems = cart,
                    GrandTotal = cart.Sum(x => x.Price * x.Quantity)
                };
                if(cartVM is not null)
                    return View(cartVM);
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

                var product = await _cartService.GetProductByIdAsync(productId);
                if (product is null)
                    return NotFound();

                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();
                
                var cartItem = cart.Where(x => x.ProductId == productId).FirstOrDefault();

                if (cartItem is null)
                    cart.Add(new CartItemDto(product));
               
                else
                {
                    cartItem.Quantity += 1;
                }

                HttpContext.Session.SetJson("Cart", cart);
                return RedirectToAction("Index");
            }
            catch (Exception ex) { return View(); }
        }
    }
}
