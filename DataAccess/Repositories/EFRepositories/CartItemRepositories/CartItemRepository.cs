using DataAccess.Repositories.Data;
using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.CartItemRepositories
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartItemByProductIdAsync(int productId)
        {
            try
            {
                var item = await _context.CartItems.FirstOrDefaultAsync(x => x.ProductId == productId);
                if (item == null)
                    return null;
                return item;
            }
            catch (Exception ex) { return null; }
        }
    }
}
