using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models;
using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Health_contentController : Controller
    {
        private readonly IHealth_contentManager _manager;
        public Health_contentController(IHealth_contentManager manager)
        {
            _manager = manager;
        }

        [HttpPost, Route("add_HealthContentInfo")]
        public async Task<IActionResult> InsertHealthContentInfo(AddHealthTitleJsonStr jsonObj)
        {
            bool _isLS = false; //是否留守
            bool _isFS = false; //是否发烧
            bool _isKS = false; //是否咳嗽
            bool _isFX = false; //是否腹泻
            bool _isOT = false; //是否呕吐
            bool _isHBJ = false; //本人是否为湖北籍
            bool _isToHB = false; //1月至今是否回过湖北
            bool _isJCOrToHB = false;  //1月至今是否接触过湖北人或到过湖北省的人
            bool _isGoHomeGL14 = false;  //在工厂当地居家隔离天数是否已达14天

            List<AddHealthContentModel> contentList = jsonObj.TitleJsonStr;
            string answerStr = "";
            if (contentList.Count > 0)
            {
                foreach (var item in contentList)
                {
                    if (item.Content == "归属地点检")
                    {
                        if (item.Answer == "留守公司宿舍" || item.Answer == "留守在厂区所在市周边")
                        {
                            _isLS = true;
                        }
                    }
                    if (item.Content == "是否咳嗽")
                    {
                        if (item.Answer == "是")
                        {
                            _isKS = true;
                        }

                    }
                    if (item.Content == "是否呕吐")
                    {
                        if (item.Answer == "是")
                        {
                            _isOT = true;
                        }

                    }
                    if (item.Content == "是否腹泻")
                    {
                        if (item.Answer == "是")
                        {
                            _isFX = true;
                        }

                    }
                    if (item.Content == "是否发烧")
                    {
                        if (item.Answer == "是")
                        {
                            _isFS = true;
                        }

                    }
                    if (item.Content == "1月至今是否回过湖北")
                    {
                        if (item.Answer == "是")
                        {
                            _isToHB = true;
                        }

                    }
                    if (item.Content == "1月至今是否接触过湖北人或到过湖北省的人")
                    {
                        if (item.Answer == "是")
                        {
                            _isJCOrToHB = true;
                        }

                    }
                    if (item.Content == "是否为湖北籍")
                    {
                        if (item.Answer == "是")
                        {
                            _isHBJ = true;
                        }

                    }
                    if (item.Content == "在工厂当地居家隔离是否已达14天")
                    {
                        if (item.Answer == "是")
                        {
                            _isGoHomeGL14 = true;
                        }

                    }
                    string str = item.Content + ":" + item.Answer;
                    str += ";";
                    answerStr += str;
                }
            }

            var returnToFactory = IsReturnToFactoryJudgementVoid(_isLS, _isFS, _isKS, _isFX, _isOT, _isHBJ, _isToHB, _isJCOrToHB, _isGoHomeGL14);

            var condition = new Health_content_Model();
            condition.TitleId = Guid.NewGuid().ToString("N");
            condition.Answer = answerStr;
            condition.ContentId = Guid.NewGuid().ToString();
            condition.CreateTime = DateTime.Now;
            condition.Creator = contentList[0].Creator;
            condition.TitleType = contentList[0].TitleType;
            condition.CreateName = contentList[0].CreateName;
            if (returnToFactory.Start == "OK")
            {
                condition.IsPass = true;
            }
            else
            {
                condition.IsPass = false;
            }
            condition.NotPassReson = returnToFactory.Massage;
            var query = new QueryData<Health_content_Model>();
            query.Criteria = condition;

            var result = await _manager.InsertHealthContentMaAsync(query);

            return Ok(result);
        }


        [HttpPost, Route("get_HealthStaffCount")]
        public async Task<IActionResult> GetHealthStaffCount(GetHealthStaffCountModel model)
        {
            var condition = new HealthStaffCountQuery_Model();
            condition.date = model.date;
            condition.userNo = model.userNo;

            var query = new QueryData<HealthStaffCountQuery_Model>();
            query.Criteria = condition;
            query.PageModel = model.PageModel;

            var result = await _manager.GetHealthStaffCountAsync(query);

            return Ok(result);
        }


        public ReturnToFactoryJudgement IsReturnToFactoryJudgementVoid(bool isLS, bool isFS, bool isKS, bool isFX, bool isOT, bool isHBJ, bool isToHB, bool isJCOrToHB, bool isGoHomeGL14)
        {
            ReturnToFactoryJudgement returnToFactory = new ReturnToFactoryJudgement();
            try
            {
                //1.判断是否留守
                if (isLS)
                {
                    returnToFactory.Start = "OK";
                    returnToFactory.Massage = "";
                }
                else
                {
                    //如果1月至今回过湖北或者1月至今接触过湖北人或到过湖北省的人
                    if (isToHB || isJCOrToHB)
                    {
                        //并且时湖北籍
                        if (isHBJ)
                        {
                            returnToFactory.Start = "NG";
                            returnToFactory.Massage = "报告政府隔离";
                        }
                        //不是湖北籍没有发烧、咳嗽、腹泻、呕吐
                        else if (!isHBJ && !isFS && !isKS && !isFX && !isOT)
                        {
                            returnToFactory.Start = "NG";
                            returnToFactory.Massage = "公司安排隔离14天";
                        }
                        //不是湖北籍有发烧或者咳嗽或者腹泻或者呕吐
                        else if (!isHBJ && isFS || isKS || isFX || !isOT)
                        {
                            returnToFactory.Start = "NG";
                            returnToFactory.Massage = "报告政府隔离";
                        }
                    }
                    //如果1月至今没有回过湖北或者1月至今没有接触过湖北人或到过湖北省的人
                    else
                    {
                        //有发烧或者咳嗽或者腹泻或者呕吐
                        if (isFS || isKS || isFX || isOT)
                        {
                            returnToFactory.Start = "NG";
                            returnToFactory.Massage = "送医，无问题隔离区14天。";
                        }
                        //没有发烧或者咳嗽或者腹泻或者呕吐
                        else
                        {
                            //在工厂当地居家隔离天数已达14天
                            if (isGoHomeGL14)
                            {
                                returnToFactory.Start = "OK";
                                returnToFactory.Massage = "";
                            }
                            //在工厂当地居家隔离天数没有达14天
                            else
                            {
                                returnToFactory.Start = "NG";
                                returnToFactory.Massage = "医学观察区/居家隔离，14天";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return returnToFactory;
        }


        /// <summary>
        /// 根据工号和日期获取体检填写详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("get_HealthContentDetail")]
        public async Task<IActionResult> GetHealthContentDetail(GetHealthContentDetailModel model)
        {
            var condition = new HealthStaffCountQuery_Model();
            condition.date = model.Date;
            condition.userNo = model.StaffNo;

            var query = new QueryData<HealthStaffCountQuery_Model>();
            query.Criteria = condition;

            var result = await _manager.GetHealthContentDetailInfoAsync(query);

            return Ok(result);
        }



        [HttpPost, Route("get_TeamLeaderQuery")]
        public async Task<IActionResult> TeamLeaderQueryTeamInfo(TeamLeaderQueryTeamModel model)
        {
            var condition = new GetTeamLeaderQueryModel();
            condition.date = model.date;
            condition.userNo = model.userNo;
            condition.teamNO = model.teamNO;
            condition.teamName = model.teamName;

            var query = new QueryData<GetTeamLeaderQueryModel>();
            query.Criteria = condition;
            query.PageModel = model.PageModel;

            var result = await _manager.TeamLeaderQueryInfoAsync(query);

            return Ok(result);
        }
    }
}