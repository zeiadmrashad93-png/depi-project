﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.domain.Entities
{
    public class MenuItem:Entity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string? Recipe { get; set; }
        public decimal Price { get; set; }
        public int Calories { get; set; }
        public int Fats { get; set; }
        public int Protein { get; set; }
        public int Carbohydrates { get; set; }
        public string Picture { get; set; }
        public string? Video { get; set; }
        public TimeSpan PreparationTime { get; set; }
    }
}
