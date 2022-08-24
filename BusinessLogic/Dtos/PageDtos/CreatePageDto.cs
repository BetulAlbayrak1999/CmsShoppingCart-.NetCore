using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos.PageDtos
{
    public class CreatePageDto
    {
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Sorting { get; set; }
        public string Slug { get; set; }
       
    }
}
