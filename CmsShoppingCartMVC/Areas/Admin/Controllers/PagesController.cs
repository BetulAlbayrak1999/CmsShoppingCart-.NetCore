using BusinessLogic.Dtos.PageDtos;
using BusinessLogic.Services.PageServices;
using BusinessLogic.Validations.FluentValidations.Page;
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

        //GET/admin/pages
        public async Task<IActionResult> Index()
        {
            try
            {
               var viewPageList =  await _pageService.GetAllAsync();
                if(viewPageList == null)
                    return NotFound();
               return View(viewPageList);
            }catch(Exception ex)
            {
                return View();
            }
        }

        //GET/admin/pages/details/6
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var viewPage = await _pageService.GetByIdAsync(Id);
                if(viewPage == null)
                    return NotFound();
                return View(viewPage);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET/admin/pages/create
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create(CreatePageDto item)
        {
            try
            {
                //validation
                var validator = new CreatePageDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    item.Slug = item.Title.ToLower().Replace(" ", "-");
                    item.Sorting = 100;
                    var itemBySlug = await _pageService.GetBySlugAsync(item.Slug);

                    if (itemBySlug != null)
                    {
                        ModelState.AddModelError("", "This Title already exists");
                        return View(item);
                    }
                     var viewPage = await _pageService.CreateAsync(item);
                     if (viewPage == false)
                        return NotFound();
                    TempData["Success"] = "The page has been added!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    return View(item);
                }
            }
            catch (Exception ex)
            {
                return View(item);
            }
       }

        //GET/admin/pages/update
        public IActionResult Update() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePageDto item)
        {
            try
            {
                //validation
                var validator = new UpdatePageDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    var viewPage = await _pageService.UpdateAsync(item);
                    if (viewPage == false)
                        return BadRequest();
                    TempData["Success"] = "The page has been added!";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in validationResult.Errors)
                        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                    return View(item);
                }
            }
            catch (Exception ex)
            {
                return View(item);
            }
        }
    }
}
