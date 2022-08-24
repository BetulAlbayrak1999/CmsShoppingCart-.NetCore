using BusinessLogic.Services.PageServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;
        public PagesController(IPageService pageService)
        {
            _pageService =  pageService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
               var viewPageList =  await _pageService.GetAllAsync();
               return View(viewPageList);
            }catch(Exception ex)
            {
                return View();
            }
        }
    }
}
