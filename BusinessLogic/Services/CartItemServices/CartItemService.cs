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
using DataAccess.Repositories.EFRepositories.ProductRepositories;
using BusinessLogic.Dtos.ProductDtos;

namespace BusinessLogic.Services.CartItemServices
{
    public class CartItemService : ICartItemService
    {
        #region Field and Ctor
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _autoMapper;
        public CartItemService(ICartItemRepository CartItemRepository, IProductRepository ProductRepository, IMapper autoMapper)
        {
            _cartItemRepository = CartItemRepository;
            _autoMapper = autoMapper;
            _productRepository = ProductRepository; 
        }
        #endregion


        public async Task<CartItemDto> GetByIdAsync(int Id)
        {
            try
            {
                if (Id > 0)
                {
                    CartItem item = await _cartItemRepository.GetByIdAsync(Id);
                    if (item is not null)
                    {
                        //mapping
                        CartItemDto mappedItem = _autoMapper.Map<CartItemDto>(item);

                        return mappedItem;
                    }
                    return null;
                }

                { return null; }

            }
            catch (Exception ex) { return null; }
        }


        public async Task<bool> UpdateAsync(CartItemDto item)
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

        public async Task<GetProductDto> GetProductByIdAsync(int productId)
        {
            try
            {
                if (productId > 0)
                {
                    Product item = await _productRepository.GetByIdAsync(productId);
                    if (item is not null)
                    {
                        //mapping
                        GetProductDto mappedItem = _autoMapper.Map<GetProductDto>(item);

                        return mappedItem;
                    }
                    return null;
                }

                { return null; }

            }
            catch (Exception ex) { return null; }
        }


        #endregion

        public async Task<CartItemDto> GetCartItemByProductIdAsync(int productId)
        {
            try
            {
                if (productId > 0)
                {
                    CartItem item = await _cartItemRepository.GetCartItemByProductIdAsync(productId);
                    if (item is not null)
                    {
                        //mapping
                        CartItemDto mappedItem = _autoMapper.Map<CartItemDto>(item);

                        return mappedItem;
                    }
                    return null;
                }

                { return null; }

            }
            catch (Exception ex) { return null; }
        }
    }
}
