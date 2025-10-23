using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace befit.infrastructure.Identity
{
    internal class AuthenticationManager:IAuthenticationManager
    {
        private UserManager<AuthenticationUser> _userManager;

        public AuthenticationManager(UserManager<AuthenticationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> CheckCredentials(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            if (await _userManager.CheckPasswordAsync(user, password))
                return user.Id;

            return null;
        }

        public async Task<string?> CreateUser(string email, string firstName, string lastName,
            string phoneNumber, string password, string role)
        {
            AuthenticationUser user = new AuthenticationUser
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email
            };

            var isUserCreated = (await _userManager.CreateAsync(user)).Succeeded;

            if (isUserCreated && (await _userManager.AddToRoleAsync(user, role)).Succeeded)
                return user.Id;

            return null;
        }

        public async Task<string?> GetRoleById(string id)
        {
            AuthenticationUser? user = await _userManager.FindByIdAsync(id);

            if (user != null)
                return (await _userManager.GetRolesAsync(user))[0];

            return null;
        }
    }
}
