using BusinessLogic.Dtos.PageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PageServices
{
    public interface IPageService
    {
        public Task<IEnumerable<PageDto>> GetAllActivatedAsync();

        public Task<IEnumerable<PageDto>> GetAllUnActivatedAsync();

        public Task<IEnumerable<PageDto>> GetAllAsync();

        public Task<bool> UpdateAsync(PageDto item);

        public Task<bool> ActivateAsync(int Id);

        public Task<bool> UnActivateAsync(int Id);
        public Task<PageDto> GetByIdAsync(int Id);

        public Task<bool> CreateAsync(PageDto item);
    }
}
