using AutoMapper;
using BusinessLogic.Dtos.PageDtos;
using BusinessLogic.Services.PageServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPageService _pageService;
        private readonly IMapper _autoMapper;
        public PagesController(IPageService pageService, IMapper autoMapper)
        {
            _pageService = pageService;
            _autoMapper = autoMapper;
        }

        // Get / or /Slug
        public async Task<IActionResult> Page(string slug)
        {
            try
            {
                if(slug != null)
                {
                    GetPageDto viewPage = await _pageService.GetBySlugAsync(slug);
                    if (viewPage == null)
                        return NotFound();
                    return View(viewPage);
                }
                GetPageDto homePage = await _pageService.GetBySlugAsync("home");
                if (homePage == null)
                    return NotFound();
                return View(homePage);
            }
            catch(Exception ex)
            {
                return View();
            }
        }
    }
}
