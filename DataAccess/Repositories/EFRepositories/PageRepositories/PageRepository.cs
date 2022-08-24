using DataAccess.Repositories.Data;
using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.PageRepositories
{
    public class PageRepository : BaseRepository<Page>, IPageRepository
    {
        private readonly ApplicationDbContext _context;
        public PageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Page> GetBySlugAsync(string slug)
        {
            try
            {
                var item = await _context.Pages.FirstOrDefaultAsync(x=> x.Slug == slug);
                if (item == null)
                    return null;
                return item;
            }catch(Exception ex) { return null; }
        }
    }
}
