using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.application.Options
{
    public class MenuItemsAdminOptions:MenuItemsGeneralOptions
    {
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
