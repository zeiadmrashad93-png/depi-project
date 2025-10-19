using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.application.DTOs.MenuItems
{
    public class MenuItemDetailChefDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Recipe { get; set; }
        public string Picture { get; set; }
        public string? Video { get; set; }
        public TimeSpan PreparationTime { get; set; }
    }
}
