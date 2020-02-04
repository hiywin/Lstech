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
            List<AddHealthContentModel> contentList = jsonObj.TitleJsonStr;
            string answerStr = "";
            if (contentList.Count > 0)
            {
                foreach (var item in contentList)
                {
                    string str = item.Content + ":" + item.Answer;
                    str += ";";
                    answerStr += str;
                }
            }
            var condition = new Health_content_Model();
            condition.TitleId = Guid.NewGuid().ToString("N");
            condition.Answer = answerStr;
            condition.ContentId = Guid.NewGuid().ToString();
            condition.CreateTime = contentList[0].CreateTime;
            condition.Creator = contentList[0].Creator;
            condition.TitleType = contentList[0].TitleType;
            condition.CreateName = contentList[0].CreateName;

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
    }
}