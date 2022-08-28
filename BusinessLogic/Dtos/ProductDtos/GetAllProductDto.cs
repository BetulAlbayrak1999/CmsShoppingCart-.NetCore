using Entity.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos.ProductDtos
{
    public class GetAllProductDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
      
        public string Name { get; set; }

        public string Slug { get; set; }


        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public string Image { get; set; }

        public virtual Category Category { get; set; }

        public IFormFile ImageUpload { get; set; }
    }
}
