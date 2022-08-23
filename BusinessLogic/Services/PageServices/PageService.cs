using DataAccess.Repositories.EFRepositories.PageRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.PageServices
{
    public class PageService: IPageService
    {
        private readonly IPageRepository _pageService;
        public PageService()
        {

        }
    }
}
