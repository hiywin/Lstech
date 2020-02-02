using Lstech.Common.Helpers;
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

            flag = FrameOperaters.Init();

            return flag;
        }

    }
}
