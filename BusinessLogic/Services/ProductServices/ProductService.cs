using AutoMapper;
using BusinessLogic.Dtos.CategoryDtos;
using BusinessLogic.Dtos.PaginationDtos;
using BusinessLogic.Dtos.ProductDtos;
using BusinessLogic.Validations.ValidationAttributes;
using DataAccess.Repositories.EFRepositories.CategoryRepositories;
using DataAccess.Repositories.EFRepositories.ProductRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.ProductServices
{

    public class ProductService : IProductService
    {
        #region Field and Ctor
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _autoMapper;
        public ProductService(IProductRepository ProductRepository, IMapper autoMapper, ICategoryRepository categoryRepository)
        {
            _productRepository = ProductRepository;
            _autoMapper = autoMapper;
            _categoryRepository = categoryRepository;
        }
        #endregion


        public Task<bool> ActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }


        [FileExtensionValidation] 
        public async Task<bool> CreateAsync(CreateProductDto item)
        {
            try
            {

                if (item is not null)
                {
                    //mapping
                    Product mappedItem = _autoMapper.Map<Product>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _productRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return true;
                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Product Errors " + "\n" + ex.Message); }

        }

        public Task<IEnumerable<GetProductDto>> GetAllActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetAllProductDto>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                @params.PageRange = 6;

                IEnumerable<Product> items = await _productRepository.GetAllByAsync();
                IEnumerable<GetAllProductDto> result = _autoMapper.Map<IEnumerable<Product>, IEnumerable<GetAllProductDto>>(items)
                    .OrderBy(x=> x.Id)
                    .Skip((@params.PageNumber - 1) * @params.PageRange)
                    .Take(@params.PageRange);
                @params.TotalPages = (int)Math.Ceiling((decimal)items.Count()/ @params.PageRange);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<GetProductDto>> GetAllUnActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetProductDto> GetByIdAsync(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    Product item = await _productRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        GetProductDto mappedItem = _autoMapper.Map<GetProductDto>(item);

                        return mappedItem;
                    }
                    return null;
                }

                { return null; }

            }
            catch (Exception ex) { return null; }
        }

        public Task<bool> UnActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(UpdateProductDto item)
        {
            try
            {
                var getItem = await _productRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //mapping
                    Product mappedItem = _autoMapper.Map<Product>(item);

                    bool IsUpdated = await _productRepository.UpdateAsync(mappedItem);
                    if (IsUpdated == true)
                        return true;

                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Product Errors " + "\n" + ex.Message); }

        }


        #region DeleteAsync
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                bool IsDeleted = await _productRepository.DeleteAsync(Id);
                if (IsDeleted)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion


        public async Task<IEnumerable<GetAllCategoryDto>> GetAllCategoryAsync()
        {
            try
            {
                IEnumerable<Category> items = await _categoryRepository.GetAllByAsync();
                IEnumerable<GetAllCategoryDto> result = _autoMapper.Map<IEnumerable<Category>, IEnumerable<GetAllCategoryDto>>(items)
                    .OrderBy(x => x.Sorting);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<GetProductDto> GetBySlugAsync(string slug)
        {
            try
            {
                var itemBySlug = await _productRepository.GetBySlugAsync(slug);
                if (itemBySlug == null)
                    return null;

                //mapping
                GetProductDto mappedItem = _autoMapper.Map<GetProductDto>(itemBySlug);

                return mappedItem;
            }
            catch (Exception ex) { return null; }
        }

        public async Task<IEnumerable<GetAllProductDto>> GetAllProductsByCategoryAsync(PaginationParamsWithCategoryDto @params)
        {
            try
            {
                @params.PageRange = 6;
                var categoty = await _categoryRepository.GetBySlugAsync(@params.CategorySlug);
                if (categoty is not null)
                    @params.CategoryName = categoty.Name;
                var products = await _productRepository.GetAllProductsByCategoryAsync(@params.CategorySlug);
                if (products == null)
                    return null;
                var mappedList = _autoMapper.Map<IEnumerable<GetAllProductDto>>(products)
                    .OrderBy(x => x.Id)
                    .Skip((@params.PageNumber - 1) * @params.PageRange)
                    .Take(@params.PageRange);
                @params.TotalPages = (int)Math.Ceiling((decimal)products.Count() / @params.PageRange);

                return mappedList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
