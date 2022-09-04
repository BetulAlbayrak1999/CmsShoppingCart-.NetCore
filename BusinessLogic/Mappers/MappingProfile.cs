using AutoMapper;
using BusinessLogic.Dtos.CartItemDtos;
using BusinessLogic.Dtos.CategoryDtos;
using BusinessLogic.Dtos.PageDtos;
using BusinessLogic.Dtos.ProductDtos;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Mappers
{
    public class MappingProfile: Profile
    {

        public MappingProfile()
        {
            #region Page
            CreateMap<Page, GetPageDto>().ReverseMap();
            CreateMap<Page, GetAllPageDto>().ReverseMap();
            CreateMap<Page, CreatePageDto>().ReverseMap();
            CreateMap<UpdatePageDto, GetPageDto>().ReverseMap();
            CreateMap<UpdatePageDto, Page>().ReverseMap();
            #endregion

            #region Page
            CreateMap<Category, GetCategoryDto>().ReverseMap();
            CreateMap<Category, GetAllCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, GetCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
            #endregion


            #region Product
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, GetAllProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, GetProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            #endregion

            #region User
           
            CreateMap<User, AppUser>().ReverseMap();
            #endregion


            #region CartItem
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            #endregion
        }

    }
}
