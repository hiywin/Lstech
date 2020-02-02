using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class JwtIssuserOptions
    {
        /// <summary>
        /// "iss" (Issuser) Claim - The "iss" (issuser) claim identifies the principal that issued the JWT.
        /// 签发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// "sub" (Subject) Claim - The "sub" (subject) claim identifies the principal that is the subject of the JWT.
        /// 面向的用户
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// "aud" (Aydience) Claim - The "aud" (audience) claim identifies the recipients that the JWT is intended for.
        /// 接收方
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// "nbf" (Not Before) Claim - The "nbf" (not before) claim identifies the time before which the JWT Must not be accepted for processing.
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;
        /// <summary>
        /// "iat" (Issused At) Claim - The "iat" (issused at) claim identifies the time at which the JWT was issued.
        /// 签发时间
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;
        /// <summary>
        /// Set the timespan the token will be valid for (defualt is 120 min).
        /// 过期时长
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(60 * 24);
        /// <summary>
        /// "exp" (Expiration Time) Claim - The "exp" (expiration time) claim identifies the expiration time on or after which the JWT must be accepted for proccessing.
        /// 过期时间戳
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        /// <summary>
        /// "jti" (JWT ID) Claim (default ID is a GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());
        /// <summary>
        /// The signing key to use when geneating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
