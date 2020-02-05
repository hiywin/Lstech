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
        /// <summary>
        /// 组长查看组员填写
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<GroupLeaderViewHealthModel>> GetHealthStaffCountAsync(QueryData<HealthStaffCountQuery_Model> query)
        {
            var lr = new ListResult<GroupLeaderViewHealthModel>();
            List<Health_staff_Model> health_Staffs = null;
            GroupLeaderViewHealthModel healthModel = null;
            var dt = DateTime.Now;

            var queryCt = new GetHealthStaffCountQuery();
            queryCt.date = query.Criteria.date;
            queryCt.userNo = query.Criteria.userNo;

            //先获取本组所有组员填写信息（统计本组未填写数量）
            var queryAll = new QueryData<GetHealthStaffCountQuery>();
            PageModel page = new PageModel();
            page.PageIndex = 1;
            page.PageSize = 1000;

            queryAll.Criteria = queryCt;
            queryAll.PageModel = page;
            var resAll = await HealthMobileOperaters.HealthContentOperater.GetHealthStaffCount(queryAll);  ///获取组员填写次数
            if (resAll.HasErr)
            {
                lr.SetInfo(resAll.ErrMsg, resAll.ErrCode);
                return lr;
            }
            else
            {
                healthModel = new GroupLeaderViewHealthModel();
                int totalCt = 0;
                int wtxCT = 0;
                if (resAll.Data.Count > 0)
                {
                    totalCt = resAll.Data.Count;
                    foreach (var itemA in resAll.Data)
                    {
                        if (itemA.iswrite == "0")
                        {
                            wtxCT += 1;
                        }
                    }
                    healthModel.NotFilledCount = wtxCT;
                    healthModel.TotalCount = totalCt;
                }
            }



            //获取组员填写（分页）
            var queryHs = new QueryData<GetHealthStaffCountQuery>();
            queryHs.Criteria = queryCt;
            queryHs.PageModel = query.PageModel;
            var res = await HealthMobileOperaters.HealthContentOperater.GetHealthStaffCount(queryHs);  ///获取组员填写次数
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    health_Staffs = new List<Health_staff_Model>();
                    foreach (var item in res.Data)
                    {
                        Health_staff_Model staff_Model = new Health_staff_Model();
                        staff_Model.StaffNo = item.StaffNo;
                        staff_Model.STAFFName = item.STAFFName;
                        staff_Model.iswrite = item.iswrite;
                        health_Staffs.Add(staff_Model);
                    }
                }
                healthModel.health_Staffs = health_Staffs;
                lr.Data = healthModel;
                lr.PageModel = res.PageInfo;
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }


        /// <summary>
        /// 提交体检内容详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
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
                contentQuery.titleId = query.Criteria.TitleId;
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
