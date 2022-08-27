using AutoMapper;
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
        private readonly IMapper _autoMapper;
        public PagesController(IPageService pageService, IMapper autoMapper)
        {
            _pageService =  pageService;
            _autoMapper = autoMapper;
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
                        ModelState.AddModelError("", "This page already exists");
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
        public async Task<IActionResult> Update(int id) 
        {
            try 
            {
                var item = await _pageService.GetByIdAsync(id);
                
                if(item == null)
                    return NotFound();
                var mappedItem = _autoMapper.Map<UpdatePageDto>(item);
                return View(mappedItem);
            }
            catch(Exception ex)
            { return View(); }
        }

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
                    item.Slug = item.Id == 1 ? "home" : item.Title.ToLower().Replace(" ", "-");
                    var itemBySlug = await _pageService.GetBySlugAsync(item.Slug);
                    if(itemBySlug != null && item.Id != itemBySlug.Id)
                    {
                        ModelState.AddModelError("", "This page already exists");
                        return View(item);
                    }
                    var viewPage = await _pageService.UpdateAsync(item);
                    if (viewPage == false)
                        return BadRequest();

                    TempData["Success"] = "The page has been updated!";
                    return RedirectToAction("Update", new {id = item.Id});
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

        // GET /admin/pages/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _pageService.GetByIdAsync(id);

            if (item == null)
            {
                TempData["Error"] = "The page does not exist!";
            }
            else
            {
                var viewPage = await _pageService.DeleteAsync(id);
                if (viewPage == false)
                    return BadRequest();

                TempData["Success"] = "The page has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
