using Lstech.PC.HealthService;
using Lstech.PC.IHealthService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Utility
{
    public class HealthPcOperaters
    {
        private static Dictionary<string, object> dic = new Dictionary<string, object>();

        public static bool Init()
        {
            if (!dic.ContainsKey("IHealthTitleService"))
                dic.Add("IHealthTitleService", null);
            if (!dic.ContainsKey("IHealthContentService"))
                dic.Add("IHealthContentService", null);

            return true;
        }

        public static IHealthTitleService HealthTitleOperater
        {
            get
            {
                var svr = dic["IHealthTitleService"] as IHealthTitleService;
                if (svr != null) return svr;

                svr = new HealthTitleService();

                dic["IHealthTitleService"] = svr;
                return svr;
            }
        }

        public static IHealthContentService HealthContentOperater
        {
            get
            {
                var svr = dic["IHealthContentService"] as IHealthContentService;
                if (svr != null) return svr;

                svr = new HealthContentService();

                dic["IHealthContentService"] = svr;
                return svr;
            }
        }
    }
}
