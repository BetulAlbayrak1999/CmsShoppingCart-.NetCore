using BusinessLogic.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.ViewComponents
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var viewPageList = await _categoryService.GetAllAsync();
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
