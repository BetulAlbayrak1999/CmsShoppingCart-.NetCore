using BusinessLogic.Services.CartServices;
using BusinessLogic.Services.CategoryServices;
using BusinessLogic.Services.PageServices;
using BusinessLogic.Services.ProductServices;
using DataAccess.Repositories.EFRepositories.CartRepositories;
using DataAccess.Repositories.EFRepositories.CategoryRepositories;
using DataAccess.Repositories.EFRepositories.PageRepositories;
using DataAccess.Repositories.EFRepositories.ProductRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Configrations.Extensions
{
    public static class StartupExtension
    {
        #region AddServices
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartService, CartService>();
        }
        #endregion


        #region AddRepositories
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
        }
        #endregion
    }

}