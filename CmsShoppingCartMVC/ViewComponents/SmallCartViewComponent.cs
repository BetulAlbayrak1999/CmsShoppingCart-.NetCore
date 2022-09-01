using BusinessLogic.Configrations.Extensions;
using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.ViewModels.CartViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CmsShoppingCartMVC.ViewComponents
{
    public class SmallCartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItemDto> cart = HttpContext.Session.GetJson<List<CartItemDto>>("Cart");
            SmallCartVM smallCartVM;
            if (cart == null || cart.Count == 0)
                smallCartVM = null;
            else
            {
                smallCartVM = new SmallCartVM
                {
                    NumberOfItems = cart.Sum(x=> x.Quantity),
                    TotalAmount = cart.Sum(x=> x.Quantity * x.Price)
                };
            }
            return View(smallCartVM);
        }
    }
}
