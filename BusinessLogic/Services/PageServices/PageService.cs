using AutoMapper;
using BusinessLogic.Dtos.PageDtos;
using BusinessLogic.Validations.FluentValidations.Page;
using DataAccess.Repositories.EFRepositories.PageRepositories;
using Entity.Domains;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PageServices
{
    public class PageService: IPageService
    {
        #region Field and Ctor
        private readonly IPageRepository _pageRepository;
        private readonly IMapper _autoMapper;
        public PageService(IPageRepository pageRepository, IMapper autoMapper)
        {
            _pageRepository = pageRepository;
            _autoMapper = autoMapper;
        }
        #endregion


        public Task<bool> ActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }


        public async Task<GetPageDto> GetBySlugAsync(string slug)
        {
            try
            {
                var itemBySlug = await _pageRepository.GetBySlugAsync(slug);
                if (itemBySlug == null)
                    return null;

                //mapping
                GetPageDto mappedItem = _autoMapper.Map<GetPageDto>(itemBySlug);

                return mappedItem;
            }
            catch(Exception ex) { return null; }
        }
        public async Task<bool> CreateAsync(CreatePageDto item)
        {
            try
            {
                
                if (item is not null)
                {
                    //mapping
                    Page mappedItem = _autoMapper.Map<Page>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _pageRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return true;
                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Page Errors " +  "\n" + ex.Message); }

        }

        public Task<IEnumerable<GetPageDto>> GetAllActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetAllPageDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Page> items = await _pageRepository.GetAllByAsync();
                IEnumerable<GetAllPageDto> result = _autoMapper.Map<IEnumerable<Page>, IEnumerable<GetAllPageDto>>(items).OrderBy(x=> x.Sorting);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<GetPageDto>> GetAllUnActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetPageDto> GetByIdAsync(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    Page item = await _pageRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        GetPageDto mappedItem = _autoMapper.Map<GetPageDto>(item);

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

        public async Task<bool> UpdateAsync(UpdatePageDto item)
        {
            try
            {
                var getItem = await _pageRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //mapping
                    Page mappedItem = _autoMapper.Map<Page>(item);
                   
                    bool IsUpdated = await _pageRepository.UpdateAsync(mappedItem);
                    if (IsUpdated == true)
                        return true;

                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("Page Errors " + "\n" + ex.Message); }

        }


        #region DeleteAsync
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                bool IsDeleted = await _pageRepository.DeleteAsync(Id);
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


        #region RecodeAsync
        public async Task<bool> RecoderAsync(int[] id)
        {
            try
            {
                int pageNumber = 1;  //homa page gonna be 0
                foreach(var pageId in id)
                {
                    Page page = await _pageRepository.GetByIdAsync(pageId);
                    page.Sorting = pageNumber;
                   var IsUpdated = await _pageRepository.UpdateAsync(page);
                    if (IsUpdated)
                    {
                        pageNumber++;
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion
    }
}
