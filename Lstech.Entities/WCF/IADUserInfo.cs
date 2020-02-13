using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Entities.WCF
{
    public interface IADUserInfo
    {
        string UserNo { get; set; }
        string UserName { get; set; }
        string ADAccount { get; set; }
    }
}
