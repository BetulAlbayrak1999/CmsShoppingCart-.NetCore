﻿using AutoMapper;
using BusinessLogic.Dtos.ProductDtos;
using BusinessLogic.Services.ProductServices;
using BusinessLogic.Validations.FluentValidations.Product;
using BusinessLogic.Validations.ValidationAttributes;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _autoMapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductService ProductService, IMapper autoMapper, IWebHostEnvironment webHostEnvironment)
        {
            _productService = ProductService;
            _autoMapper = autoMapper;
            _webHostEnvironment = webHostEnvironment;   
        }

        //GET/admin/Products
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewProductList = await _productService.GetAllAsync();
                if (viewProductList == null)
                    return NotFound();
                return View(viewProductList);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        
        //GET/admin/Products/details/6
        public async Task<IActionResult> Details(int Id)
        {
            try
            {
                var viewProduct = await _productService.GetByIdAsync(Id);
                if (viewProduct == null)
                    return NotFound();
                return View(viewProduct);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //GET/admin/Products/create
        public async Task<IActionResult> Create()
        {
            try
            {
                var CategoryDB = await _productService.GetAllCategoryAsync();
                
                List<SelectListItem> categories = (from x in CategoryDB
                                                         select new SelectListItem
                                                         {
                                                             Text = x.Name,
                                                             Value = x.Id.ToString()
                                                         }).ToList();
                ViewBag.categories = categories;

                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [FileExtensionValidation]

        public async Task<IActionResult> Create(CreateProductDto item)
        {
            try
            {
                var CategoryDB = await _productService.GetAllCategoryAsync();

                List<SelectListItem> categories = (from x in CategoryDB
                                                   select new SelectListItem
                                                   {
                                                       Text = x.Name,
                                                       Value = x.Id.ToString()
                                                   }).ToList();
                ViewBag.categories = categories;

                //validation
                var validator = new CreateProductDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    item.Slug = item.Name.ToLower().Replace(" ", "-");
                    var itemBySlug = await _productService.GetBySlugAsync(item.Slug);

                    if (itemBySlug != null)
                    {
                        ModelState.AddModelError("", "This Product already exists");
                        return View(item);
                    }

                    #region upload file
                    string imageName = "noimage.png";
                    if(item.ImageUpload != null)
                    {
                        string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");
                        imageName = Guid.NewGuid().ToString() + "_" + item.ImageUpload.FileName;
                        string filePath = Path.Combine(uploadsDir, imageName);
                        FileStream fileStream = new FileStream (filePath, FileMode.Create);
                        await item.ImageUpload.CopyToAsync(fileStream);
                        fileStream.Close();
                    }
                    #endregion

                    item.Image = imageName;
                    var viewProduct = await _productService.CreateAsync(item);
                    if (viewProduct == false)
                        return NotFound();
                    TempData["Success"] = "The Product has been added!";
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

        //GET/admin/Products/update
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var item = await _productService.GetByIdAsync(id);

                if (item == null)
                    return NotFound();
                var mappedItem = _autoMapper.Map<UpdateProductDto>(item);
                return View(mappedItem);
            }
            catch (Exception ex)
            { return View(); }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateProductDto item)
        {
            try
            {
                //validation
                var validator = new UpdateProductDtoValidator();
                var validationResult = validator.Validate(item);
                if (validationResult.IsValid)
                {
                    var viewProduct = await _productService.UpdateAsync(item);
                    if (viewProduct == false)
                        return BadRequest();

                    TempData["Success"] = "The Product has been updated!";
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

        // GET /admin/Products/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _productService.GetByIdAsync(id);

            if (item == null)
            {
                TempData["Error"] = "The Product does not exist!";
            }
            else
            {
                var viewProduct = await _productService.DeleteAsync(id);
                if (viewProduct == false)
                    return BadRequest();

                TempData["Success"] = "The Product has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}
