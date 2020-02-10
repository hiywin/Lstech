using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthStaffDeleteViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
