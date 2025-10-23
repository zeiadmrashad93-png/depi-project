using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using befit.application.DTOs.Authentication;
using befit.application.Enums;
using befit.domain.Contracts;

namespace befit.application.Services
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthenticationService(IAuthenticationManager authenticationManager, ITokenGenerator tokenGenerator)
        {
            _authenticationManager = authenticationManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<string?> AuthenticateUser(LoginDto dto)
        {
            string? userId = await _authenticationManager.CheckCredentials(dto.Email, dto.Password);

            if (userId != null)
            {
                string? userRole = await _authenticationManager.GetRoleById(userId);

                return _tokenGenerator.GenerateToken(userId, dto.Email, userRole ?? Roles.USER.ToString());
            }

            return null;
        }

        public async Task<string?> RegisterNewUser(RegisterDto dto)
        {
            string? userId = await _authenticationManager.CreateUser(dto.Email, dto.FirstName, dto.LastName,
                dto.PhoneNumber, dto.Password, dto.Role);

            if (userId != null)
                return _tokenGenerator.GenerateToken(userId, dto.Email, dto.Role);

            return null;
        }
    }
}
