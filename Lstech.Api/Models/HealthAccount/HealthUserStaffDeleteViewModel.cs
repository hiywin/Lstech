using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthUserStaffDeleteViewModel
    {
        [Required]
        public string UserNo { get; set; }
        [Required]
        public string StaffNo { get; set; }
    }
}
