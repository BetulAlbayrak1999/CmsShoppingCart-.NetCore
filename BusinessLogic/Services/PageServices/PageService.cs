using AutoMapper;
using BusinessLogic.Dtos.PageDtos;
using DataAccess.Repositories.EFRepositories.PageRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public Task<bool> CreateAsync(PageDto item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PageDto>> GetAllActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PageDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<Page> items = await _pageRepository.GetAllByAsync();
                IEnumerable<PageDto> result = _autoMapper.Map<IEnumerable<Page>, IEnumerable<PageDto>>(items).OrderBy(x=> x.Sorting);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<PageDto>> GetAllUnActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PageDto> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(PageDto item)
        {
            throw new NotImplementedException();
        }
    }
}
