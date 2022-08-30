using BusinessLogic.Services.PageServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.ViewComponents
{
    public class MainMenuViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        public MainMenuViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var viewPageList = await _pageService.GetAllAsync();
                if (viewPageList != null)
                    return View(viewPageList);
                return View(null);

            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}
