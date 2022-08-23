using Entity.Domains.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Domains
{
    public class Page: BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public int Sorting { get; set; }
    }
}
