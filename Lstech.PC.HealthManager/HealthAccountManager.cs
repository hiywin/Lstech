using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.IWCFService.Structs;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthAccountManager : IHealthAccountManager
    {
        public async Task<ErrData<IHealthUser>> HealthUserLoginAsync(QueryData<HealthUserQuery> query)
        {
            var result = new ErrData<IHealthUser>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthAccountOperater.GetHealthUserPageAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (res.Data.Count <= 0)
                {
                    result.SetInfo("账号无权限，请联系管理员。", -102);
                }
                else
                {
                    var queryEx = new QueryData<WcfADUserGuidQuery>()
                    {
                        Criteria = new WcfADUserGuidQuery()
                        {
                            UserName = query.Criteria.AdAccount,
                            Password = query.Criteria.Pwd
                        }
                    };
                    var resGuid = await WCFOperators.TlgChinaOperater.GetUserADGuidAsync(queryEx);
                    if (string.IsNullOrEmpty(resGuid.Data))
                    {
                        result.SetInfo("用户名或密码错误！", -102);
                    }
                    else
                    {
                        var userModel = res.Data.FirstOrDefault();
                        result.SetInfo(userModel, "登录成功！", 200);
                    }
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
