using Lstech.Common.Helpers;
using Lstech.Common.Wcf;
using Lstech.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Utility.Base
{
    public class InitOperators
    {
        public static bool Init(ISqlConnModel sqlConn)
        {
            bool flag;
            MysqlHelper.MysqlConn = sqlConn.MysqlConn;
            MssqlHelper.ConnCommon = sqlConn.MssqlConn;
            WcfInvoke.TlgChinaServiceUrl = sqlConn.TlgChinaServiceUrl;
            WcfInvoke.LstechServiceUrl = sqlConn.LstechServiceUrl;

            flag = FrameOperaters.Init();
            flag = HealthPcOperaters.Init();
            flag = HealthMobileOperaters.Init();
            flag = WCFOperators.Init();

            return flag;
        }

    }
}
