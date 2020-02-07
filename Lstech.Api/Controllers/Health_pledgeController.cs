using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models;
using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Health_pledgeController : BaseController
    {
        private readonly IHealth_pledgeManager _manager;

        public Health_pledgeController(IHealth_pledgeManager manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// 根据工号判断是否确认承诺书
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("get_HealthpledgeInfoByNo")]
        public async Task<IActionResult> GetHealthpledgeInfoByNo(GetHealthPledgeInfoModel model)
        {
            var condition = new GetHealthPledgeInfoQuery();
            condition.StaffNo = model.StaffNo;
            condition.StaffName = model.StaffName;

            var query = new QueryData<GetHealthPledgeInfoQuery>();
            query.Criteria = condition;

            var result = await _manager.GetHealthPledgeByNoMaAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 保存承诺书确认信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("add_HealthPledgeInfo")]
        public async Task<IActionResult> InsertHealthPledgeInfo(InsertHealthPledgeInfoModel model)
        {
            var condition = new InsertHealthPledgeInfoQuery();
            condition.StaffNo = model.StaffNo;
            condition.IsSign = model.IsSign;
            condition.PledgeType = model.PledgeType;
            condition.SignTime = DateTime.Now;
            condition.StaffName = model.StaffName;

            var query = new QueryData<InsertHealthPledgeInfoQuery>();
            query.Criteria = condition;

            var result = await _manager.InsertHealthPledgeInfoMaAsync(query);

            return Ok(result);
        }
    }
}