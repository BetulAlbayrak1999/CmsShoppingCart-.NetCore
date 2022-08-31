using DataAccess.Repositories.Data;
using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
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
    }
}
