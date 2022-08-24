using BusinessLogic.Dtos.PageDtos;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PageServices
{
    public interface IPageService
    {
        public Task<IEnumerable<GetPageDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetPageDto>> GetAllUnActivatedAsync();


        public Task<IEnumerable<GetAllPageDto>> GetAllAsync();

        public Task<bool> UpdateAsync(UpdatePageDto item);

        public Task<bool> ActivateAsync(int Id);

        public Task<bool> UnActivateAsync(int Id);
        
        public Task<GetPageDto> GetByIdAsync(int Id);
        public Task<GetPageDto> GetBySlugAsync(string slug);

        public Task<bool> CreateAsync(CreatePageDto item);
    }
}
