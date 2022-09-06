using DataAccess.Repositories.EFRepositories.BaseRepositories;
using Entity.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.EFRepositories.CartRepositories
{
    public interface ICartRepository : IBaseRepository<CartItem>
    {

    }
}
