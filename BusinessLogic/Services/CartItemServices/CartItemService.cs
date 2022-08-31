using AutoMapper;
using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.ViewModels.CartItemViewModels;
using DataAccess.Repositories.EFRepositories.CartItemRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Configrations.Extensions;
using Microsoft.AspNetCore.Mvc;
namespace BusinessLogic.Services.CartItemServices
{
    public class CartItemService : ICartItemService
    {
        #region Field and Ctor
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _autoMapper;
        public CartItemService(ICartItemRepository CartItemRepository, IMapper autoMapper)
        {
            _cartItemRepository = CartItemRepository;
            _autoMapper = autoMapper;
        }
        #endregion


        public Task<bool> ActivateAsync(int Id)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> CreateAsync(CreateCartItemDto item)
        {
            try
            {

                if (item is not null)
                {
                    //mapping
                    CartItem mappedItem = _autoMapper.Map<CartItem>(item);
                    mappedItem.CreatedDate = DateTime.Now;
                    var IsCreated = await _cartItemRepository.CreateAsync(mappedItem);
                    if (IsCreated == true)
                        return true;
                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("CartItem Errors " + "\n" + ex.Message); }

        }

        public Task<IEnumerable<GetCartItemDto>> GetAllActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetAllCartItemDto>> GetAllAsync()
        {
            try
            {
                IEnumerable<CartItem> items = await _cartItemRepository.GetAllByAsync();
                IEnumerable<GetAllCartItemDto> result = _autoMapper.Map<IEnumerable<CartItem>, IEnumerable<GetAllCartItemDto>>(items);

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<IEnumerable<GetCartItemDto>> GetAllUnActivatedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetCartItemDto> GetByIdAsync(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    CartItem item = await _cartItemRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        GetCartItemDto mappedItem = _autoMapper.Map<GetCartItemDto>(item);

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

        public async Task<bool> UpdateAsync(UpdateCartItemDto item)
        {
            try
            {
                var getItem = await _cartItemRepository.GetByIdAsync(item.Id);
                if (item is not null && getItem is not null)
                {
                    //mapping
                    CartItem mappedItem = _autoMapper.Map<CartItem>(item);

                    bool IsUpdated = await _cartItemRepository.UpdateAsync(mappedItem);
                    if (IsUpdated == true)
                        return true;

                    return false;
                }

                { return false; }

            }
            catch (Exception ex) { throw new Exception("CartItem Errors " + "\n" + ex.Message); }

        }


        #region DeleteAsync
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                bool IsDeleted = await _cartItemRepository.DeleteAsync(Id);
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
    }
}
