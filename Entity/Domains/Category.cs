using Entity.Domains.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domains
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Sorting { get; set; }
    }
}
