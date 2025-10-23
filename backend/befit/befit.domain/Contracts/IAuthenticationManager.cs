using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.domain.Contracts
{
    public interface IAuthenticationManager
    {
        public Task<string?> GetRoleById(string id);

        public Task<string?> CreateUser(string email, string firstName, string lastName,
            string phoneNumber, string password, string role);

        public Task<string?> CheckCredentials(string email, string password);
    }
}
