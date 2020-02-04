using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models
{
    public class GetHealthStaffCountModel
    {
        public string date { get; set; }
        public string userNo { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
