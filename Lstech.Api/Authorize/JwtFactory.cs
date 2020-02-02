using Lstech.Api.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Lstech.Api.Authorize
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuserOptions _jwtOptions;
        public JwtFactory(IOptions<JwtIssuserOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            //ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userNo, ClaimsIdentity identity)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userNo),
                new Claim(JwtRegisteredClaimNames.Jti,await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat,ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),ClaimValueTypes.Integer64),
                identity.FindFirst("userNo"),
                identity.FindFirst(ClaimTypes.Name),
                identity.FindFirst("isAdmin"),
            };

            //Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenModel
            {
                auth_token = encodeJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                token_type = "Bearer"
            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public ClaimsIdentity GenerateClaimsIdentity(LoginUser user)
        {
            var claimsIdentity = new ClaimsIdentity(new GenericIdentity(user.UserNo, "Token"));
            claimsIdentity.AddClaim(new Claim("userNo", user.UserNo));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            claimsIdentity.AddClaim(new Claim("isAdmin", user.IsAdmin.ToString()));

            return claimsIdentity;
        }

        #region Extensions

        public static long ToUnixEpochDate(DateTime date) =>
            (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        #endregion
    }
}
