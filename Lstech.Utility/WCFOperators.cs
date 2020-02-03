using Lstech.IWCFService;
using Lstech.WCFService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Utility
{
    public class WCFOperators
    {
        private static Dictionary<string, object> dic = new Dictionary<string, object>();

        public static bool Init()
        {
            if (!dic.ContainsKey("ITlgChinaWebService"))
                dic.Add("ITlgChinaWebService", null);

            return true;
        }

        public static ITlgChinaWebService TlgChinaOperater
        {
            get
            {
                var svr = dic["ITlgChinaWebService"] as ITlgChinaWebService;
                if (svr != null) return svr;

                svr = new TlgChinaWebService();

                dic["ITlgChinaWebService"] = svr;
                return svr;
            }
        }
    }
}
