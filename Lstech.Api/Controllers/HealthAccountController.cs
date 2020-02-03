using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Authorize;
using Lstech.Api.Models;
using Lstech.Api.Models.HealthAccount;
using Lstech.Common.Data;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HealthAccountController : BaseController
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly IHealthAccountManager _manager;
        public HealthAccountController(IJwtFactory jwtFactory, IHealthAccountManager manager)
        {
            _jwtFactory = jwtFactory;
            _manager = manager;
        }

        [HttpPost, Route("health_login")]
        public async Task<IActionResult> HealthLogin(HealthLoginViewModel model)
        {
            var result = new ErrData<string>();

            var query = new QueryData<HealthUserQuery>()
            {
                Criteria = new HealthUserQuery()
                {
                    AdAccount = model.UserName,
                    Pwd = model.Password
                }
            };
            var res = await _manager.HealthUserLoginAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.Msg, res.Code);
            }
            else
            {
                var user = new LoginUser();
                user.UserNo = res.Data.UserNo;
                user.UserName = res.Data.UserName;
                user.AdAccount = res.Data.AdAccount;

                var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
                var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIdentity);
                var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

                result.SetInfo(token.auth_token, "成功", 200);
            }

            return Ok(result);
        }

        [Authorize, HttpGet, Route("get_user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = new LoginUser();
            user = CurrentUser;

            return Ok(user);
        }
    }
}