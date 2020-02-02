using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    public class Health_contentManager : IHealth_contentManager
    {
        public async Task<ErrData<bool>> InsertHealthContentMaAsync(QueryData<Health_content_Model> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                InsertHealthContentQuery contentQuery = new InsertHealthContentQuery();
                contentQuery.Answer = query.Criteria.Answer;
                contentQuery.ContentId = query.Criteria.ContentId;
                contentQuery.CreateName = query.Criteria.CreateName;
                contentQuery.CreateTime = query.Criteria.CreateTime;
                contentQuery.Creator = query.Criteria.Creator;
                contentQuery.TitleId = query.Criteria.TitleId;
                contentQuery.TitleType = query.Criteria.TitleType;


                var queryXQ = new QueryData<InsertHealthContentQuery>();
                queryXQ.Criteria = contentQuery;

                var res = await HealthMobileOperaters.HealthContentOperater.InsertHealthContent(queryXQ);
                if (res.HasErr)
                {
                    result.SetInfo(false, "体检详细信息提交失败", res.ErrCode);
                }
                else
                {
                    result.SetInfo(true, "成功");
                }
            }
            catch (Exception ex)
            {
                result.SetInfo(ex.ToString(), -500);
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
