using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.Dtos.ProductDtos;
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
        
        public Task<bool> UpdateAsync(CartItemDto item);

        public Task<CartItemDto> GetByIdAsync(int Id);
        public Task<CartItemDto> GetCartItemByProductIdAsync(int productId);

        public Task<GetProductDto> GetProductByIdAsync(int productId);

        public Task<bool> DeleteAsync(int Id);

    }
}
