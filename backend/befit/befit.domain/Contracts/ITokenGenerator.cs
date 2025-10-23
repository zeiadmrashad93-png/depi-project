using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.domain.Contracts
{
    public interface ITokenGenerator
    {
        string GenerateToken(string userId, string email, string role);
    }
}
