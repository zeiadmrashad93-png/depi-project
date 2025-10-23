using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.DTOs.Authentication;

namespace befit.application.Contracts
{
    public interface IAuthenticationService
    {
        Task<string?> AuthenticateUser(LoginDto dto);

        Task<string?> RegisterNewUser(RegisterDto dto);
    }
}
