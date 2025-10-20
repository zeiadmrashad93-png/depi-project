using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.application.Contracts
{
    public interface IOptions
    {
        int PageNo { get; set; }
        int ItemsPerPage { get; set; }
        string? SortBy { get; set; }
        bool? IsAscending { get; set; }
    }
}
