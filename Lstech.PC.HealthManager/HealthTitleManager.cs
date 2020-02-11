using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.Models.Health;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthTitleManager : IHealthTitleManager
    {
        public async Task<ListResult<HealthTitle>> GetHealthTitlePageAsync(QueryData<HealthTitleQuery> query)
        {
            var lr = new ListResult<HealthTitle>();
            var dt = DateTime.Now;

            var queryEx = query.Criteria;
            queryEx.IsParentQuery = true;
            queryEx.ParentId = string.Empty;
            //queryEx.IsShow = true;
            query.Criteria = queryEx;
            var res = await HealthPcOperaters.HealthTitleOperater.GetHealthTitlePageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                var querySub = new QueryData<HealthTitleQuery>()
                {
                    Criteria = new HealthTitleQuery()
                    {
                        IsParentQuery = false
                    }
                };
                var resSub = await HealthPcOperaters.HealthTitleOperater.GetHealthTitleAllAsync(querySub);
                foreach (var item in res.Data)
                {
                    var info = new HealthTitle();
                    info.Id = item.Id;
                    info.TitleId = item.TitleId;
                    info.Content = item.Content;
                    info.Type = item.Type;
                    info.IsMustFill = item.IsMustFill;
                    info.ParentId = item.ParentId;
                    info.Creator = item.Creator;
                    info.CreateTime = item.CreateTime;
                    info.Updator = item.Updator;
                    info.UpdateTime = item.UpdateTime;
                    info.Sort = item.Sort;
                    info.IsShow = item.IsShow;
                    var lstSub = resSub.Data.FindAll(p => p.ParentId == item.TitleId);
                    foreach (var tem in lstSub)
                    {
                        var infoTem = new HealthTitle();
                        infoTem.Id = tem.Id;
                        infoTem.TitleId = tem.TitleId;
                        infoTem.Content = tem.Content;
                        infoTem.Type = tem.Type;
                        infoTem.IsMustFill = tem.IsMustFill;
                        infoTem.ParentId = tem.ParentId;
                        infoTem.Creator = tem.Creator;
                        infoTem.CreateTime = tem.CreateTime;
                        infoTem.Updator = tem.Updator;
                        infoTem.UpdateTime = tem.UpdateTime;
                        infoTem.Sort = tem.Sort;
                        infoTem.IsShow = tem.IsShow;
                        info.LstSubTitle.Add(infoTem);
                    }
                    lr.Results.Add(info);
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
