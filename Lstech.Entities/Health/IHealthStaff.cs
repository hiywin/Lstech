using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    public interface IHealthStaff
    {
        int Id { get; set; }
        string StaffNo { get; set; }
        string StaffName { get; set; }
        string GroupType { get; set; }
        string GroupLeader { get; set; }
        string GroupLeaderNo { get; set; }
        string AggLeader { get; set; }
        string AggLeaderNo { get; set; }
        string CommandLeader { get; set; }
        string CommondLeaderNo { get; set; }
        string HrLeader { get; set; }
        string HrLeaderNo { get; set; }
    }
}
