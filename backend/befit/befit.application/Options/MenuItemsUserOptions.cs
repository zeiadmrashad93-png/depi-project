using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;

namespace befit.application.Options
{
    public class MenuItemsUserOptions : MenuItemsGeneralOptions
    {
        public int CategoryId { get; set; }
        public int? MaxCalories { get; set; }
        public int? MinCalories { get; set; }
        public int? MaxCarbohydrates { get; set; }
        public int? MinCarbohydrates { get; set; }
        public int? MaxProtein { get; set; }
        public int? MinProtein { get; set; }
        public int? MaxFat { get; set; }
        public int? MinFat { get; set; }
    }
}
