using BusinessLogic.Dtos.CategoryDtos;
using BusinessLogic.Dtos.PaginationDtos;
using BusinessLogic.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.ProductServices
{
    public interface IProductService
    {
        public Task<IEnumerable<GetProductDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetProductDto>> GetAllUnActivatedAsync();


        public Task<IEnumerable<GetAllProductDto>> GetAllAsync(PaginationParams @params);
        public Task<IEnumerable<GetAllCategoryDto>> GetAllCategoryAsync();

        public Task<IEnumerable<GetAllProductDto>> GetAllProductsByCategoryAsync(PaginationParamsWithCategoryDto @params);


        public Task<bool> UpdateAsync(UpdateProductDto item);

        public Task<bool> ActivateAsync(int Id);

        public Task<bool> UnActivateAsync(int Id);

        public Task<GetProductDto> GetByIdAsync(int Id);
        
        public Task<bool> CreateAsync(CreateProductDto item);

        public Task<bool> DeleteAsync(int Id);

        public Task<GetProductDto> GetBySlugAsync(string slug);

    }
}
