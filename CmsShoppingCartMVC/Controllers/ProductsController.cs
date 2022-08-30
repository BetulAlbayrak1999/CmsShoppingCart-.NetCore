using AutoMapper;
using BusinessLogic.Dtos.PaginationDtos;
using BusinessLogic.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CmsShoppingCartMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _autoMapper;

        public ProductsController(IProductService ProductService, IMapper autoMapper)
        {
            _productService = ProductService;
            _autoMapper = autoMapper;
        }

        // GET /products
        [Route("Products")]
        public async Task<IActionResult> Index(int pageNumber = 1)
        {
            try
            {
                PaginationParams @params = new PaginationParams { };
                @params.PageNumber = pageNumber;
                var viewProductList = await _productService.GetAllAsync(@params);
                if (viewProductList != null)
                {
                    ViewBag.PageNumber = @params.PageNumber;
                    ViewBag.PageRange = @params.PageRange;
                    ViewBag.TotalPages = @params.TotalPages;
                    return View(viewProductList);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET /products/category
        public async Task<IActionResult> ProductsByCategory(string categorySlug, int pageNumber = 1)
        {
            try
            {
                PaginationParamsWithCategoryDto @params = new PaginationParamsWithCategoryDto { };
                @params.CategorySlug = categorySlug;
                @params.PageNumber = pageNumber;
                var viewProductList = await _productService.GetAllProductsByCategoryAsync(@params);
                if (viewProductList != null)
                {
                    ViewBag.PageNumber = @params.PageNumber;
                    ViewBag.PageRange = @params.PageRange;
                    ViewBag.TotalPages = @params.TotalPages;
                    ViewBag.CategoryName = @params.CategoryName;
                    ViewBag.CategorySlug = @params.CategorySlug;
                    return View(viewProductList);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
