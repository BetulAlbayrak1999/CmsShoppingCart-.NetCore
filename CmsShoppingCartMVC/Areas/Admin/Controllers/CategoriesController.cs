using AutoMapper;
using BusinessLogic.Dtos.CategoryDtos;
using BusinessLogic.Services.CategoryServices;
using BusinessLogic.Validations.FluentValidations.Category;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _autoMapper;
        public CategoriesController(ICategoryService categoryService, IMapper autoMapper)
        {
            _categoryService = categoryService;
            _autoMapper = autoMapper;
        }

        //GET/admin/Categories
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewCategoryList = await _categoryService.GetAllAsync();
                if (viewCategoryList == null)
                    return NotFound();
                return View(viewCategoryList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET/admin/Categories/details/6
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var viewCategory = await _categoryService.GetByIdAsync(Id);
                if (viewCategory == null)
                    return NotFound();
                return View(viewCategory);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET/admin/Categorys/create
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto item)
        {
            try
            {
                //validation
                var validator = new CreateCategoryDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    item.Slug = item.Name.ToLower().Replace(" ", "-");
                    item.Sorting = 100;
                    var itemBySlug = await _categoryService.GetBySlugAsync(item.Slug);

                    if (itemBySlug != null)
                    {
                        ModelState.AddModelError("", "This Category already exists");
                         return View(item);
                    }
                    var viewCategory = await _categoryService.CreateAsync(item);
                    if (viewCategory == false)
                        return NotFound();
                    TempData["Success"] = "The Category has been added!";
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

        //GET/admin/Categorys/update
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var item = await _categoryService.GetByIdAsync(id);

                if (item == null)
                    return NotFound();
                var mappedItem = _autoMapper.Map<UpdateCategoryDto>(item);
                return View(mappedItem);
            }
            catch (Exception ex)
            { return View(); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryDto item)
        {
            try
            {
                //validation
                var validator = new UpdateCategoryDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    item.Slug = item.Name.ToLower().Replace(" ", "-");
                    var itemBySlug = await _categoryService.GetBySlugAsync(item.Slug);
                    if (itemBySlug != null)
                    {
                        ModelState.AddModelError("", "This Category already exists");
                        return View(item);
                    }
                    var viewCategory = await _categoryService.UpdateAsync(item);
                    if (viewCategory == false)
                        return BadRequest();

                    TempData["Success"] = "The Category has been updated!";
                    return RedirectToAction("Update", new { id = item.Id });
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

        // GET /admin/Categorys/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _categoryService.GetByIdAsync(id);

            if (item == null)
            {
                TempData["Error"] = "The Category does not exist!";
            }
            else
            {
                var viewCategory = await _categoryService.DeleteAsync(id);
                if (viewCategory == false)
                    return BadRequest();

                TempData["Success"] = "The Category has been deleted!";
            }

            return RedirectToAction("Index");
        }


        // POST /admin/categories/Reorder
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            try
            {
                bool isDone = await _categoryService.RecoderAsync(id);
                if (isDone)
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
