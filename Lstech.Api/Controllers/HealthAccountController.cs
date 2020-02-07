using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Authorize;
using Lstech.Api.Models;
using Lstech.Api.Models.HealthAccount;
using Lstech.Common.Data;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthManager.Structs;
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
                user.IsAdmin = res.Data.IsAdmin;

                var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
                var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIdentity);
                var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

                result.SetInfo(token.auth_token, "成功", 200);
            }

            return Ok(result);
        }

        /// <summary>
        /// 获取登录人列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_health_users")]
        public async Task<IActionResult> GetHealthUsers(HealthUsersQueryViewModel model)
        {
            var query = new QueryData<HealthUserQuery>()
            {
                Criteria = new HealthUserQuery()
                {
                    AdAccount = model.AdAccount,
                    UserNo = model.UserNo,
                    UserName = model.UserName
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthUsersPageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_health_staffs")]
        public async Task<IActionResult> GetHealthStaffs(HealthStaffsQueryViewModel model)
        {
            var query = new QueryData<HealthStaffQuery>()
            {
                Criteria = new HealthStaffQuery()
                {
                    StaffNo = model.StaffNo,
                    StaffName = model.StaffName,
                    GroupType = model.GroupType,
                    GroupLeader = model.GroupLeader,
                    GroupLeaderNo = model.GroupLeaderNo,
                    AggLeader = model.AggLeader,
                    AggLeaderNo = model.AggLeaderNo,
                    CommandLeader = model.CommandLeader,
                    CommondLeaderNo = model.CommondLeaderNo,
                    HrLeader = model.HrLeader,
                    HrLeaderNo = model.HrLeaderNo
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthStaffsPageAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 保存登录用户-员工关联表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("health_user_staff_save")]
        public async Task<IActionResult> HealthUserStaffSave(HealthUserStaffAddViewModel model)
        {
            var query = new QueryData<HealthUserStaffModel>()
            {
                Criteria = new HealthUserStaffModel()
                {
                    UserNo = CurrentUser.UserNo,
                    LstStaffNo = model.LstStaffNo
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.HealthUserStaffSaveAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 删除登录用户-员工关联表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("health_user_staff_delete")]
        public async Task<IActionResult> HealthUserStaffDelete(HealthUserStaffDeleteViewModel model)
        {
            var query = new QueryData<HealthUserStaffDeleteQuery>()
            {
                Criteria = new HealthUserStaffDeleteQuery()
                {
                    UserNo = CurrentUser.UserNo,
                    StaffNo = model.StaffNo
                }
            };
            var result = await _manager.HealthUserStaffDeleteAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取当前登录账户信息
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet, Route("get_user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = new LoginUser();
            user = CurrentUser;

            return Ok(user);
        }
    }
}