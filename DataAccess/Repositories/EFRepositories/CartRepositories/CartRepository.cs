using DataAccess.Repositories.Data;
using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.CartRepositories
{
    public class CartRepository : BaseRepository<CartItem>, ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
