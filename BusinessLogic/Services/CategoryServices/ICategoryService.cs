using BusinessLogic.Dtos.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CategoryServices
{
    public interface ICategoryService
    {
        public Task<IEnumerable<GetCategoryDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetCategoryDto>> GetAllUnActivatedAsync();


        public Task<IEnumerable<GetAllCategoryDto>> GetAllAsync();

        public Task<bool> UpdateAsync(UpdateCategoryDto item);

        public Task<bool> ActivateAsync(int Id);

        public Task<bool> UnActivateAsync(int Id);

        public Task<GetCategoryDto> GetByIdAsync(int Id);

        public Task<bool> CreateAsync(CreateCategoryDto item);

        public Task<bool> DeleteAsync(int Id);

        public Task<GetCategoryDto> GetBySlugAsync(string slug);

        public Task<bool> RecoderAsync(int[] id);

    }
}
