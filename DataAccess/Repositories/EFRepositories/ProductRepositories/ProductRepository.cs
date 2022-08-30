using DataAccess.Repositories.Data;
using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.ProductRepositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Product>> GetAllByAsync(Expression<Func<Product, bool>> predicate = null)
        {
            try
            {
                if (predicate == null)
                {
                    IEnumerable<Product> items = await _context.Products.OrderBy(x => x.Id).Include(x => x.Category).ToListAsync();
                    if (items.Any())
                        return items;
                    return null;
                }
                IEnumerable<Product> itemsByCondition = await _context.Products.OrderBy(x => x.Id).Include(x => x.Category).Where(predicate).ToListAsync();
                if (itemsByCondition.Any())
                    return itemsByCondition;
                return null;

            }
            catch(Exception ex) { return null; }
        }

        public async Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(string categorySlug)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Slug == categorySlug);
                if (category == null)
                    return Enumerable.Empty<Product>();

                IEnumerable<Product> products = await _context.Products
                      .OrderByDescending(x => x.Id)
                      .Where(x => x.CategoryId == category.Id).ToListAsync();

                return products;


            }
            catch (Exception ex) { return null; }
        }

        public async Task<Product> GetBySlugAsync(string slug)
        {
            try
            {
                var item = await _context.Products.FirstOrDefaultAsync(x => x.Slug == slug);
                if (item == null)
                    return null;
                return item;
            }
            catch (Exception ex) { return null; }
        }
    }
}
