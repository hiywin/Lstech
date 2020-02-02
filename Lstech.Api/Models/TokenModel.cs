using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class TokenModel
    {
        public string auth_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
}
