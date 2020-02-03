using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.Health
{
    public interface IHealthUser
    {
        int Id { get; set; }
        string UserNo { get; set; }
        string UserName { get; set; }
        string AdAccount { get; set; }
        string Creator { get; set; }
        DateTime CreateTime { get; set; }
    }
}
