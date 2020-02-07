using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthStaffsQueryViewModel
    {
        public string StaffNo { get; set; }
        public string StaffName { get; set; }
        public string GroupType { get; set; }
        public string GroupLeader { get; set; }
        public string GroupLeaderNo { get; set; }
        public string AggLeader { get; set; }
        public string AggLeaderNo { get; set; }
        public string CommandLeader { get; set; }
        public string CommondLeaderNo { get; set; }
        public string HrLeader { get; set; }
        public string HrLeaderNo { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
