using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos.PaginationDtos
{
    public class PaginationParams
    {
        public int PageNumber { get; set; } 
        public int PageRange { get; set; } 
        public int TotalPages { get; set; } 
        
    }
}
