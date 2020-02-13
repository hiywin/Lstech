using Lstech.Entities.WCF;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.WCF
{
    public class ADUserInfo: IADUserInfo
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string ADAccount { get; set; }
    }
}
