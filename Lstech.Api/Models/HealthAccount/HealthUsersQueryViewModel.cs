using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthUsersQueryViewModel
    {
        public string AdAccount { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
