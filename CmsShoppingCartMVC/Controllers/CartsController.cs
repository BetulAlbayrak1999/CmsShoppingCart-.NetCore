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

                if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")
                    return RedirectToAction("Index");

                return ViewComponent("SmallCart");
            }
            catch (Exception ex) { return View(); }
        }


        // GET /carts/decrease/5
        public IActionResult Decrease([FromRoute] int productId)
        {
            try
            {
                if (productId == 0)
                    return NotFound();

                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();

                var cartItem = cart.Where(x => x.ProductId == productId).FirstOrDefault();

                if (cartItem.Quantity > 1)
                    --cartItem.Quantity;

                else
                    cart.RemoveAll(x=> x.ProductId == productId);
                

               
                if (cart.Count == 0)
                    HttpContext.Session.Remove("Cart");
                else
                    HttpContext.Session.SetJson("Cart", cart);

                return RedirectToAction("Index");
            }
            catch (Exception ex) { return View(); }
        }

        // GET /carts/remove/5
        public IActionResult Remove([FromRoute] int productId)
        {
            try
            {
                if (productId == 0)
                    return NotFound();

                List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();

                cart.RemoveAll(x => x.ProductId == productId);


                if (cart.Count == 0)
                    HttpContext.Session.Remove("Cart");
                else
                    HttpContext.Session.SetJson("Cart", cart);

                return RedirectToAction("Index");
            }
            catch (Exception ex) { return View(); }
        }

        // GET /carts/clear
        [Route("[action]")]
        public IActionResult Clear()
        {
            try
            { 
                HttpContext.Session.Remove("Cart");

                //return RedirectToAction("Page", "Pages"); 
                //return Redirect("/"); //go to root
                if (HttpContext.Request.Headers["X-Requested-With"] != "XMLHttpRequest")//to previous page
                    return Redirect(Request.Headers["Referer"].ToString());

                return Ok();
            }
            catch (Exception ex) { return View(); }
        }
    }
}
