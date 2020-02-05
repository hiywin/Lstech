using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models;
using Lstech.Common.Data;
using Lstech.IWCFService.Structs;
using Lstech.Mobile.IHealthManager;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HealthAccount_MobileController : BaseController
    {
        private readonly IHealthAccountMobileManager _manager;
        public HealthAccount_MobileController(IHealthAccountMobileManager manager)
        {
            _manager = manager;
        }

        [HttpPost, Route("get_DDUserInfo")]
        public async Task<IActionResult> GetDDUserInfo(GetDDUserInfoModel model)
        {
            var condition = new WebServiceDDQuery();
            condition.code = model.code;
            condition.ProcessCode = model.ProcessCode;
            condition.corpid = model.corpid;

            var query = new QueryData<WebServiceDDQuery>();
            query.Criteria = condition;

            var result = await _manager.GetDDUserInfoAsync(query);

            return Ok(result);
        }
    }
}