using Lstech.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lstech.Api.Authorize
{
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userNo, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(LoginUser user);
    }
}
