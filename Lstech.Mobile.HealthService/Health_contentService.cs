using Lstech.Common.Data;
using Lstech.Common.Helpers;
using Lstech.Entities.Health;
using Lstech.Mobile.IHealthService;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthService
{
    /// <summary>
    /// 体检详细信息提交数据接口实现
    /// </summary>
    public class Health_contentService : IHealth_contentService
    {
        /// <summary>
        /// 根据工号和日期获取体检填写内容
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_content_Model>>> GetHealthContentDetailsInfoByNoAndDate(QueryData<GetHealthStaffCountQuery> query)
        {
            var lr = new DataResult<List<IHealth_content_Model>>();
            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.date) ? string.Empty : string.Format(" and CONVERT(varchar(10), CreateTime, 120) = '{0}' ", query.Criteria.date);
            condition += string.IsNullOrEmpty(query.Criteria.userNo) ? string.Empty : string.Format(" and Creator = '{0}' ", query.Criteria.userNo);
            string sql = string.Format(@"SELECT Top 1 [Id],[ContentId],[titleId],[TitleType],[Answer],[Creator],[CreateTime],[CreateName],[IsPass],[NotPassReson] FROM [dbo].[health_content] " + condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_content_Model>(dbConn, sql, "CreateTime desc");
                    lr.Data = modelList.ToList<IHealth_content_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                    lr.Data = null;
                }
            }
            return lr;
        }

        /// <summary>
        /// 组长查看组员填写（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_staff_Model>>> GetHealthStaffCount(QueryData<GetHealthStaffCountQuery> query)
        {
            var lr = new DataResult<List<IHealth_staff_Model>>();
            string sql = string.Format(@"select distinct  staff.StaffNo,staff.STAFFName,
                case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite from health_staff staff 
                left join (select * from  health_content  where (CONVERT(varchar(100), CreateTime, 23)  ='{0}'or CreateTime is null) ) as content on content.Creator=staff.StaffNo
                where   
                (staff.GroupLeaderNo='{1}' or AggLeaderNo='{1}' or CommondLeaderNo='{1}' or HrLeaderNo='{1}')", query.Criteria.date, query.Criteria.userNo);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<Health_staff_Model>(dbConn, "iswrite asc", sql, query.PageModel);
                    lr.Data = modelList.ToList<IHealth_staff_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }

        /// <summary>
        /// 获取所有组员填写
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_staff_Model>>> GetHealthStaffCount_All(QueryData<GetHealthStaffCountQuery> query)
        {
            var lr = new DataResult<List<IHealth_staff_Model>>();

            string sql = string.Format(@"select distinct  staff.StaffNo,staff.STAFFName,
                case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite from health_staff staff 
                left join (select * from  health_content  where (CONVERT(varchar(100), CreateTime, 23)  ='{0}'or CreateTime is null) ) as content on content.Creator=staff.StaffNo
                where   
                (staff.GroupLeaderNo='{1}' or AggLeaderNo='{1}' or CommondLeaderNo='{1}' or HrLeaderNo='{1}') ", query.Criteria.date, query.Criteria.userNo);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_staff_Model>(dbConn, sql, "iswrite asc");
                    lr.Data = modelList.ToList<IHealth_staff_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }



        //public async Task<DataResult<List<IHealth_staff_Model>>> GetHealthStaffCount_All(QueryData<GetTeamLeaderQueryModel> query)
        //{
        //    var lr = new DataResult<List<IHealth_staff_Model>>();

        //    string strWhere = " where 1= 1 ";
        //    strWhere += string.IsNullOrEmpty(query.Criteria.teamNO) ? string.Empty : string.Format(" and K.StaffNo = '{0}' ", query.Criteria.teamNO);
        //    strWhere += string.IsNullOrEmpty(query.Criteria.teamName) ? string.Empty : string.Format(" and K.STAFFName = '{0}' ", query.Criteria.teamName);

        //    string sql = string.Format(@"select * from (select distinct  staff.StaffNo,staff.STAFFName,
        //        case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite from health_staff staff 
        //        left join (select * from  health_content  where (CONVERT(varchar(100), CreateTime, 23)  ='{0}'or CreateTime is null) ) as content on content.Creator=staff.StaffNo
        //        where   
        //        (staff.GroupLeaderNo='{1}' or AggLeaderNo='{1}' or CommondLeaderNo='{1}' or HrLeaderNo='{1}')) K " + strWhere, query.Criteria.date, query.Criteria.userNo);
        //    using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
        //    {
        //        try
        //        {
        //            var modelList = await MssqlHelper.QueryListAsync<Health_staff_Model>(dbConn, sql, "iswrite asc");
        //            lr.Data = modelList.ToList<IHealth_staff_Model>();
        //            lr.PageInfo = query.PageModel;
        //        }
        //        catch (Exception ex)
        //        {
        //            lr.SetErr(ex, -101);
        //            lr.Data = null;
        //        }
        //    }
        //    return lr;
        //}

        /// <summary>
        /// 保存体检详细信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> InsertHealthContent(QueryData<InsertHealthContentQuery> query)
        {
            var lr = new DataResult<int>();

            string condition = string.Format(@"insert into health_content(ContentId,titleId,TitleType,Answer,Creator,CreateTime,CreateName,IsPass,NotPassReson) values(@ContentId,@titleId,@TitleType,@Answer,@Creator,@CreateTime,@CreateName,@IsPass,@NotPassReson)");
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    lr.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, condition, query.Criteria);
                    if (lr.Data < 0)
                    {
                        lr.SetErr("保存体检详情异常", lr.Data);
                    }
                    if (lr.Data == 0)
                    {
                        lr.SetErr("保存体检详情失败", -102);
                    }
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -500);
                }
            }
            return lr;
        }

        /// <summary>
        /// 组长查询（根据权限查看,带分页）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_staff_Model>>> TeamLeaderQuery(QueryData<GetTeamLeaderQueryModel> query)
        {
            var lr = new DataResult<List<IHealth_staff_Model>>();

            string strWhere = " ";
            strWhere += string.IsNullOrEmpty(query.Criteria.teamNO) ? string.Empty : string.Format(" and staff.StaffNo='{0}' ", query.Criteria.teamNO);
            strWhere += string.IsNullOrEmpty(query.Criteria.teamName) ? string.Empty : string.Format(" and  staff.StaffName='{0}' ", query.Criteria.teamName);

            string sql = string.Format(@"select distinct staff.StaffNo,staff.StaffName, case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite  
              from health_user_staff uf  left join health_staff staff on uf.StaffNo=staff.StaffNo 
              left join (select * from  health_content  where (CONVERT(varchar(100), CreateTime, 23)  ='{0}' or CreateTime is null) ) as content 
              on content.Creator=staff.StaffNo  where uf.UserNo='{1}' " + strWhere, query.Criteria.date, query.Criteria.userNo);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<Health_staff_Model>(dbConn, "iswrite asc", sql, query.PageModel);
                    lr.Data = modelList.ToList<IHealth_staff_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }


        /// <summary>
        /// 组长查询（根据权限查看,获取所有）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IHealth_staff_Model>>> TeamLeaderQuery_All(QueryData<GetTeamLeaderQueryModel> query)
        {
            var lr = new DataResult<List<IHealth_staff_Model>>();
            string strWhere = " ";
            strWhere += string.IsNullOrEmpty(query.Criteria.teamNO) ? string.Empty : string.Format(" and staff.StaffNo='{0}' ", query.Criteria.teamNO);
            strWhere += string.IsNullOrEmpty(query.Criteria.teamName) ? string.Empty : string.Format(" and  staff.StaffName='{0}' ", query.Criteria.teamName);

            string sql = string.Format(@"select distinct staff.StaffNo,staff.StaffName, case   when content.Contentid is not null then 1 when content.Contentid is null then 0 else 0 end iswrite  
              from health_user_staff uf  left join health_staff staff on uf.StaffNo=staff.StaffNo 
              left join (select * from  health_content  where (CONVERT(varchar(100), CreateTime, 23)  ='{0}' or CreateTime is null) ) as content 
              on content.Creator=staff.StaffNo  where uf.UserNo='{1}' " + strWhere, query.Criteria.date, query.Criteria.userNo);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<Health_staff_Model>(dbConn, sql, "iswrite asc");
                    lr.Data = modelList.ToList<IHealth_staff_Model>();
                    lr.PageInfo = query.PageModel;
                }
                catch (Exception ex)
                {
                    lr.SetErr(ex, -101);
                    lr.Data = null;
                }
            }
            return lr;
        }
    }
}
