using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.application.DTOs.MenuItems
{
    public class MenuItemDetailUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Picture {  get; set; }
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Carbohydrates { get; set; }
        public int Protein { get; set; }
    }
}
