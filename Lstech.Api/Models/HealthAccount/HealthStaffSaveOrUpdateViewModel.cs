using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthStaffSaveOrUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string StaffNo { get; set; }
        [Required]
        public string StaffName { get; set; }
        [Required]
        public string GroupType { get; set; }
        [Required]
        public string GroupLeader { get; set; }
        [Required]
        public string GroupLeaderNo { get; set; }
        [Required]
        public string AggLeader { get; set; }
        [Required]
        public string AggLeaderNo { get; set; }
        [Required]
        public string CommandLeader { get; set; }
        [Required]
        public string CommondLeaderNo { get; set; }
        [Required]
        public string HrLeader { get; set; }
        [Required]
        public string HrLeaderNo { get; set; }
    }
}
