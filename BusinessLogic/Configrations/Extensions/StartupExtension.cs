using BusinessLogic.Services.PageServices;
using DataAccess.Repositories.EFRepositories.PageRepositories;
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
        }
        #endregion

        #region AddRepositories
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPageRepository, PageRepository>();
        }
        #endregion
    }

}