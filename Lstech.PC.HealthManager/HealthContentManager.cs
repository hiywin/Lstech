using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthContentManager : IHealthContentManager
    {
        public async Task<ListResult<IHealthContent>> GetHealthContentPageAsync(QueryData<HealthContentQuery> query)
        {
            var lr = new ListResult<IHealthContent>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthContentOperater.GetHealthContentPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    lr.Results.Add(item);
                }
                lr.SetInfo("成功", 200);
                lr.PageModel = res.PageInfo;
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        public async Task<ListResult<IHealthContent>> GetHealthContentPageAsyncEx(QueryData<HealthContentMainQuery> query)
        {
            var lr = new ListResult<IHealthContent>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthContentOperater.GetHealthContentMainPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                //var queryTitle=new QueryData<HealthTitleQuery>()
                //{

                //}
                //var resTitle=await HealthPcOperaters.HealthTitleOperater.GetHealthTitleAllAsync()

                //foreach (var item in res.Data)
                //{
                //    lr.Results.Add(item);
                //}
                //lr.SetInfo("成功", 200);
                //lr.PageModel = res.PageInfo;
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
