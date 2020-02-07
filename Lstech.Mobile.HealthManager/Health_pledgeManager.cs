using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    /// <summary>
    /// 承诺书确认
    /// </summary>
    public class Health_pledgeManager : IHealth_pledgeManager
    {
        /// <summary>
        /// 根据工号获取承诺书确认信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<bool>> GetHealthPledgeByNoMaAsync(QueryData<GetHealthPledgeInfoQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                //到组织结构表中验证工号的正确
                //GetHealthStaffInfoQuery staffInfoQuery = new GetHealthStaffInfoQuery();
                //staffInfoQuery.StaffNo = query.Criteria.StaffNo;
                //var queryYZ = new QueryData<GetHealthStaffInfoQuery>();
                //queryYZ.Criteria = staffInfoQuery;
                //var resYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryYZ);
                //if (resYZ.HasErr)
                //{
                //    result.SetInfo(false, "验证工号是否归属组织表失败", resYZ.ErrCode);
                //    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                //    return result;
                //}
                //else
                //{
                //    if (resYZ.Data == null || resYZ.Data.Count == 0)
                //    {
                //        result.SetInfo(false, "未查询到工号：" + queryYZ.Criteria.StaffNo + "组织人员信息", -111);
                //        result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                //        return result;
                //    }
                //}



                //----------------
                //到组织结构表中验证工号的正确
                GetHealthStaffInfoQuery staffInfoByNoQuery = new GetHealthStaffInfoQuery();
                staffInfoByNoQuery.StaffNo = query.Criteria.StaffNo;
                staffInfoByNoQuery.StaffName = "";

                var queryByNoYZ = new QueryData<GetHealthStaffInfoQuery>();
                queryByNoYZ.Criteria = staffInfoByNoQuery;
                var resYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryByNoYZ);
                if (resYZ.HasErr)
                {
                    result.SetInfo(false, "通过工号验证工号是否归属组织表失败", resYZ.ErrCode);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                else
                {
                    if (resYZ.Data == null || resYZ.Data.Count == 0)
                    {
                        //如果通过工号查询不到就用姓名查询
                        GetHealthStaffInfoQuery staffInfoByNameQuery = new GetHealthStaffInfoQuery();
                        staffInfoByNameQuery.StaffNo = "";
                        staffInfoByNameQuery.StaffName = query.Criteria.StaffName;

                        var queryByNameYZ = new QueryData<GetHealthStaffInfoQuery>();
                        queryByNameYZ.Criteria = staffInfoByNameQuery;


                        var resNameYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryByNameYZ);
                        if (resNameYZ.HasErr)
                        {
                            result.SetInfo(false, "通过姓名验证工号是否归属组织表失败", resYZ.ErrCode);
                            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                            return result;
                        }
                        else
                        {
                            //工号姓名都没有查到
                            if (resNameYZ.Data == null || resNameYZ.Data.Count == 0)
                            {
                                result.SetInfo(false, "未查询到工号：" + query.Criteria.StaffNo + "组织人员信息，请找当地HR确认", -111);
                                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                                return result;
                            }
                            //通过姓名查询到了
                            else
                            {
                                result.SetInfo(false, "工号：" + query.Criteria.StaffNo + "异常,请联系HR：" + resNameYZ.Data[0].HrLeader, -111);
                                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                                return result;
                            }
                        }
                    }
                }


                var pledgeInfoQuery = new GetHealthPledgeInfoQuery();
                pledgeInfoQuery.StaffNo = query.Criteria.StaffNo;

                var queryCN = new QueryData<GetHealthPledgeInfoQuery>();
                queryCN.Criteria = pledgeInfoQuery;

                var res = await HealthMobileOperaters.Health_pledgeServiceOperater.GetHealthPledgeInfo(queryCN);
                if (res.HasErr)
                {
                    result.SetInfo(false, "承诺书确认提交失败", res.ErrCode);
                }
                else
                {
                    //如果根据工号查询到确认承诺书记录返回true,代表已阅读并且已经提交
                    if (res.Data.Count > 0)
                    {
                        result.SetInfo(true, "承诺书已确认", 200);  
                    }
                    else
                    {
                        result.SetInfo(false, "承诺书未确认", -101);
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetInfo(ex.ToString(), -500);
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }


        /// <summary>
        /// 保存确认承诺书
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<bool>> InsertHealthPledgeInfoMaAsync(QueryData<InsertHealthPledgeInfoQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                //到组织结构表中验证工号的正确
                //GetHealthStaffInfoQuery staffInfoQuery = new GetHealthStaffInfoQuery();
                //staffInfoQuery.StaffNo = query.Criteria.StaffNo;
                //var queryYZ = new QueryData<GetHealthStaffInfoQuery>();
                //queryYZ.Criteria = staffInfoQuery;
                //var resYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryYZ);
                //if (resYZ.HasErr)
                //{
                //    result.SetInfo(false, "验证工号是否归属组织表失败", resYZ.ErrCode);
                //    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                //    return result;
                //}
                //else
                //{
                //    if (resYZ.Data == null || resYZ.Data.Count == 0)
                //    {
                //        result.SetInfo(false, "未查询到工号：" + queryYZ.Criteria.StaffNo + "组织人员信息", -111);
                //        result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                //        return result;
                //    }
                //}

                //到组织结构表中验证工号的正确
                GetHealthStaffInfoQuery staffInfoByNoQuery = new GetHealthStaffInfoQuery();
                staffInfoByNoQuery.StaffNo = query.Criteria.StaffNo;
                staffInfoByNoQuery.StaffName = "";

                var queryByNoYZ = new QueryData<GetHealthStaffInfoQuery>();
                queryByNoYZ.Criteria = staffInfoByNoQuery;
                var resYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryByNoYZ);
                if (resYZ.HasErr)
                {
                    result.SetInfo(false, "通过工号验证工号是否归属组织表失败", resYZ.ErrCode);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                else
                {
                    if (resYZ.Data == null || resYZ.Data.Count == 0)
                    {
                        //如果通过工号查询不到就用姓名查询
                        GetHealthStaffInfoQuery staffInfoByNameQuery = new GetHealthStaffInfoQuery();
                        staffInfoByNameQuery.StaffNo = "";
                        staffInfoByNameQuery.StaffName = query.Criteria.StaffName;

                        var queryByNameYZ = new QueryData<GetHealthStaffInfoQuery>();
                        queryByNameYZ.Criteria = staffInfoByNameQuery;


                        var resNameYZ = await HealthMobileOperaters.Health_staffServiceOperater.GetHealthStaffInfo(queryByNameYZ);
                        if (resNameYZ.HasErr)
                        {
                            result.SetInfo(false, "通过姓名验证工号是否归属组织表失败", resYZ.ErrCode);
                            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                            return result;
                        }
                        else
                        {
                            //工号姓名都没有查到
                            if (resNameYZ.Data == null || resNameYZ.Data.Count == 0)
                            {
                                result.SetInfo(false, "未查询到工号：" + query.Criteria.StaffNo + "组织人员信息，请找当地HR确认", -111);
                                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                                return result;
                            }
                            //通过姓名查询到了
                            else
                            {
                                result.SetInfo(false, "工号：" + query.Criteria.StaffNo + "异常,请联系HR：" + resNameYZ.Data[0].HrLeader, -111);
                                result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                                return result;
                            }
                        }
                    }
                }

                var pledgeInfoAddQuery = new InsertHealthPledgeInfoQuery();
                pledgeInfoAddQuery.StaffNo = query.Criteria.StaffNo;
                pledgeInfoAddQuery.IsSign = query.Criteria.IsSign;
                pledgeInfoAddQuery.PledgeType = query.Criteria.PledgeType;
                pledgeInfoAddQuery.SignTime = query.Criteria.SignTime;
                pledgeInfoAddQuery.StaffName = query.Criteria.StaffName;

                var queryCN = new QueryData<InsertHealthPledgeInfoQuery>();
                queryCN.Criteria = pledgeInfoAddQuery;

                var res = await HealthMobileOperaters.Health_pledgeServiceOperater.InsertHealthPledgeInfo(queryCN);
                if (res.HasErr)
                {
                    result.SetInfo(false, "保存确认承诺书失败", res.ErrCode);
                }
                else
                {
                    result.SetInfo(true, "成功", 200);
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
