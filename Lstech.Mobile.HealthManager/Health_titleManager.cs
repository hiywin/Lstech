using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using Lstech.Utility;
using System;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    public class Health_titleManager : IHealth_titleManager
    {
        /// <summary>
        /// 获取所有体检内容表头
        /// </summary>
        /// <returns></returns>
        public async Task<ListResult<Health_title_Model>> GetHealthTitleAllAsync()
        {
            var lr = new ListResult<Health_title_Model>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<GetAllHealthTitleQuery>()
            {
                Criteria = new GetAllHealthTitleQuery()
                {
                    IsShow = true
                }
            };
            var res = await HealthMobileOperaters.HealthTitleOperater.GetAllHealthTitle(queryEx);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    var info = new Health_title_Model();
                    info.Id = item.Id;
                    info.IsMustFill = item.IsMustFill;
                    info.IsShow = item.IsShow;
                    info.ParentId = item.ParentId;
                    info.Sort = item.Sort;
                    info.TitleId = item.TitleId;
                    info.Type = item.Type;
                    info.UpdateTime = item.UpdateTime;
                    info.Updator = item.Updator;
                    info.Content = item.Content;
                    info.CreateTime = item.CreateTime;
                    info.Creator = item.Creator;
                    lr.Results.Add(info);
                }
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
