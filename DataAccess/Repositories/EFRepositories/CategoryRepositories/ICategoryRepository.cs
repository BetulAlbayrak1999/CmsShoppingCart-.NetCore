using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.CategoryRepositories
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {
        public Task<Category> GetBySlugAsync(string slug);
    }
}
