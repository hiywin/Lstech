using Lstech.IWCFService;
using Lstech.Mobile.HealthService;
using Lstech.Mobile.IHealthService;
using Lstech.WCFService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Utility
{
    public class HealthMobileOperaters
    {
        private static Dictionary<string, object> dic = new Dictionary<string, object>();

        public static bool Init()
        {
            if (!dic.ContainsKey("IHealth_titleService"))
                dic.Add("IHealth_titleService", null);

            if (!dic.ContainsKey("IHealth_contentService"))
                dic.Add("IHealth_contentService", null);

            if (!dic.ContainsKey("ITlgChinaWebService"))
                dic.Add("ITlgChinaWebService", null);

            return true;
        }


        /// <summary>
        /// 体检内容表头
        /// </summary>
        public static IHealth_titleService HealthTitleOperater
        {
            get
            {
                var svr = dic["IHealth_titleService"] as IHealth_titleService;
                if (svr != null) return svr;

                svr = new Health_titleService();

                dic["IHealth_titleService"] = svr;
                return svr;
            }
        }

        /// <summary>
        /// 体检详细信息
        /// </summary>
        public static IHealth_contentService HealthContentOperater
        {
            get
            {
                var svr = dic["IHealth_contentService"] as IHealth_contentService;
                if (svr != null) return svr;

                svr = new Health_contentService();

                dic["IHealth_contentService"] = svr;
                return svr;
            }
        }

        public static ITlgChinaWebService TlgChinaWebServiceOperater
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
