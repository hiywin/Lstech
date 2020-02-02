using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class LoginUser
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
