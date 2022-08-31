using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.ViewModels.CartItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.CartItemServices
{
    public interface ICartItemService
    {
        public Task<IEnumerable<GetCartItemDto>> GetAllActivatedAsync();

        public Task<IEnumerable<GetCartItemDto>> GetAllUnActivatedAsync();


        public Task<IEnumerable<GetAllCartItemDto>> GetAllAsync();

        public Task<bool> UpdateAsync(UpdateCartItemDto item);

        public Task<bool> ActivateAsync(int Id);

        public Task<bool> UnActivateAsync(int Id);

        public Task<GetCartItemDto> GetByIdAsync(int Id);

        public Task<bool> CreateAsync(CreateCartItemDto item);

        public Task<bool> DeleteAsync(int Id);

    }
}
