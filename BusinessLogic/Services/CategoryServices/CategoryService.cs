using AutoMapper;
using BusinessLogic.Dtos.CategoryDtos;
using DataAccess.Repositories.EFRepositories.CategoryRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        #region Field and Ctor
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _autoMapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper autoMapper)
        {
            _categoryRepository = categoryRepository;
            _autoMapper = autoMapper;
        }
        #endregion


        public Task<bool> ActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> CreateAsync(CreateCategoryDto item)
        {
            try
            {

                if (item is not null)
                {
                    //mapping
                    Category mappedItem = _autoMapper.Map<Category>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _categoryRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return true;
                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Category Errors " + "\n" + ex.Message); }

        }

        public Task<IEnumerable<GetCategoryDto>> GetAllActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetAllCategoryDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Category> items = await _categoryRepository.GetAllByAsync();
                IEnumerable<GetAllCategoryDto> result = _autoMapper.Map<IEnumerable<Category>, IEnumerable<GetAllCategoryDto>>(items).OrderBy(x => x.Sorting);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<GetCategoryDto>> GetAllUnActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetCategoryDto> GetByIdAsync(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    Category item = await _categoryRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        GetCategoryDto mappedItem = _autoMapper.Map<GetCategoryDto>(item);

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

        public async Task<bool> UpdateAsync(UpdateCategoryDto item)
        {
            try
            {
                var getItem = await _categoryRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //mapping
                    Category mappedItem = _autoMapper.Map<Category>(item);

                    bool IsUpdated = await _categoryRepository.UpdateAsync(mappedItem);
                    if (IsUpdated == true)
                        return true;

                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Category Errors " + "\n" + ex.Message); }

        }


        #region DeleteAsync
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                bool IsDeleted = await _categoryRepository.DeleteAsync(Id);
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


        public async Task<GetCategoryDto> GetBySlugAsync(string slug)
        {
            try
            {
                var itemBySlug = await _categoryRepository.GetBySlugAsync(slug);
                if (itemBySlug == null)
                    return null;

                //mapping
                GetCategoryDto mappedItem = _autoMapper.Map<GetCategoryDto>(itemBySlug);

                return mappedItem;
            }
            catch (Exception ex) { return null; }
        }

        #region RecodeAsync
        public async Task<bool> RecoderAsync(int[] id)
        {
            try
            {
                int categoryNumber = 1;  //homa page gonna be 0
                foreach (var categoryId in id)
                {
                    Category category = await _categoryRepository.GetByIdAsync(categoryId);
                    category.Sorting = categoryNumber;
                    var IsUpdated = await _categoryRepository.UpdateAsync(category);
                    if (IsUpdated)
                    {
                        categoryNumber++;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
