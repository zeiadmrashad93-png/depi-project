using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.domain.Entities
{
    public class Category:Entity<int>
    {
        public string Name { get; set; }
        public IEnumerable<MenuItem> menuItems { get; set; }
    }
}
