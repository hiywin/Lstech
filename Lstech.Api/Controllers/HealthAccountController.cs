using System;
using System.Collections.Generic;
using System.IO;
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
            var res = await _manager.HealthUserLoginExAsync(query);
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
                    UserNo = CurrentUser.IsAdmin ? model.UserNo : CurrentUser.UserNo,
                    UserName = CurrentUser.IsAdmin ? model.UserName : string.Empty
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthUsersPageAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取人员结构列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                    HrLeader = CurrentUser.IsAdmin ? model.HrLeader : string.Empty,
                    HrLeaderNo = CurrentUser.IsAdmin ? model.HrLeaderNo : CurrentUser.UserNo
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthStaffsPageAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 新增或修改人员结构信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("health_staff_save_or_update")]
        public async Task<IActionResult> HealthStaffSaveOrUpdate(HealthStaffSaveOrUpdateViewModel model)
        {
            var query = new QueryData<HealthStaffQuery>()
            {
                Criteria = new HealthStaffQuery()
                {
                    Id = model.Id,
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
                }
            };
            var result = await _manager.HealthStaffSaveOrUpdateAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 删除人员结构员工
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("health_staff_delete")]
        public async Task<IActionResult> HealthStaffSDelete(HealthStaffDeleteViewModel model)
        {
            var query = new QueryData<HealthStaffQuery>()
            {
                Criteria = new HealthStaffQuery()
                {
                    Id = model.Id
                }
            };
            var result = await _manager.HealthStaffDeleteAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取权限-人员结构关联列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_health_user_staff_infos")]
        public async Task<IActionResult> GetHealthUserStaffInfos(HealthUserStaffInfoViewModel model)
        {
            var query = new QueryData<HealthUserStaffInfoQuery>()
            {
                Criteria = new HealthUserStaffInfoQuery()
                {
                    UserNo = model.UpStaffNo,
                    StaffNo = model.StaffNo,
                    StaffName = model.StaffName
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthUserStaffInfoPageAsync(query);

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
                    UserNo = model.UpStaffNo,
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
                    UserNo = model.UpStaffNo,
                    StaffNo = model.StaffNo
                }
            };
            var result = await _manager.HealthUserStaffDeleteAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取有权限管理人员列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_health_power_staffs")]
        public async Task<IActionResult> GetHealthPowerStaffs(HealthPowerStaffViewModel model)
        {
            var query = new QueryData<HealthPowerStaffQuery>()
            {
                Criteria = new HealthPowerStaffQuery()
                {
                    UserNo = model.UserNo,
                    StaffName = model.UserName,
                    Creator = CurrentUser.IsAdmin ? string.Empty : CurrentUser.UserNo
                },
                Extend = new QueryExtend()
                {
                    IsAdmin = CurrentUser.IsAdmin
                },
                PageModel = model.PageModel
            };
            var result = await _manager.HealthPowerStaffPageAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 批量新增员工资料并给组长、集合组长、HR负责人添加权限-Excel导入
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [Authorize, HttpPost,Route("health_staff_impot")]
        public async Task<IActionResult> HealthStaffImport(IFormCollection form)
        {
            var stream = form?.Files[0]?.OpenReadStream();
            var query = new QueryData<Stream>()
            {
                Criteria = stream,
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.HealthStaffImportAsync(query);

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