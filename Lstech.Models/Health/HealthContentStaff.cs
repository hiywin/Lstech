﻿using Lstech.Entities.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class HealthContentStaff: IHealthContentStaff
    {
        public int Id { get; set; }
        public string ContentId { get; set; }
        public string TitleId { get; set; }
        public string Answer { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public bool? IsPass { get; set; }
        public string NotPassReson { get; set; }

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
    }
}
