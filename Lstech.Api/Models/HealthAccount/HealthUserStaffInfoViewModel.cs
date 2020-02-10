using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthUserStaffInfoViewModel
    {
        /// <summary>
        /// 上级工号
        /// </summary>
        [Required]
        public string UpStaffNo { get; set; }
        public string StaffNo { get; set; }
        public string StaffName { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
