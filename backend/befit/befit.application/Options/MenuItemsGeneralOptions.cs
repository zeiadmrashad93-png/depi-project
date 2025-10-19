using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;

namespace befit.application.Options
{
    public abstract class MenuItemsGeneralOptions : IOptions
    {
        public int PageNo { get; set; }
        public int ItemsPerPage { get; set; }
        public string? SortBy { get; set; }
        public bool? IsAscending { get; set; }
    }
}
