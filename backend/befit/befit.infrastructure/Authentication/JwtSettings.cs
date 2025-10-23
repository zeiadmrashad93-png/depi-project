using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace befit.infrastructure.Authentication
{
    internal class JwtSettings
    {
        public string Key {  get; }
        public string Issuer {  get; }
        public string Audience {  get; }
        public double Lifetime {  get; }
    }
}
