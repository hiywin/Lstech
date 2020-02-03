using Lstech.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Base
{
    public class SqlConnModel : ISqlConnModel
    {
        public string MysqlConn { get; set; }
        public string MssqlConn { get; set; }
        public string TlgChinaServiceUrl { get; set; }
        public string LstechServiceUrl { get; set; }
    }
}
