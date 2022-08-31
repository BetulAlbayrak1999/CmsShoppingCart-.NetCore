using BusinessLogic.Dtos.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ViewModels.CartItemViewModels
{
    public class CartItemVM
    {
        public List<GetCartItemDto> CartItems { get; set; }

        public decimal GrandTotal { get; set;}
    }
}
