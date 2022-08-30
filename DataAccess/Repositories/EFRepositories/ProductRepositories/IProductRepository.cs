using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.ProductRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        public Task<Product> GetBySlugAsync(string slug);
        public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(string categorySlug);

    }

}
