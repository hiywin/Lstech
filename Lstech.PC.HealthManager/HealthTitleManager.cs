using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthTitleManager : IHealthTitleManager
    {
        public async Task<ListResult<IHealthTitle>> GetHealthTitlePageAsync(QueryData<HealthTitleQuery> query)
        {
            var lr = new ListResult<IHealthTitle>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthTitleOperater.GetHealthTitlePageAsync(query);
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

        public async Task<ErrData<bool>> HealthTitleSaveAsync(QueryData<HealthTitleSaveQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var model = query.Criteria;
            model.TitleId = Guid.NewGuid().ToString("N");
            model.Creator = string.IsNullOrEmpty(query.Extend.UserNo) ? string.Empty : query.Extend.UserNo;
            query.Criteria = model;

            var res = await HealthPcOperaters.HealthTitleOperater.HealthTitleSaveAsync(query);
            if(res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "保存数据成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> HealthTitleUpdateAsync(QueryData<HealthTitleUpdateQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var model = query.Criteria;
            model.Updator = string.IsNullOrEmpty(query.Extend.UserNo) ? string.Empty : query.Extend.UserNo;
            query.Criteria = model;

            var res = await HealthPcOperaters.HealthTitleOperater.HealthTitleUpdateAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "更新数据成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<bool>> HealthTitleDeleteAsync(QueryData<HealthTitleDeleteQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthTitleOperater.HealthTitleDeleteAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(false, res.ErrMsg, res.ErrCode);
            }
            else
            {
                result.SetInfo(true, "删除数据成功！", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
