using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lstech.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    /// <summary>
    /// 基类
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// 获取远程客户端的IP地址
        /// </summary>
        /// <returns></returns>
        protected System.Net.IPAddress GetClientIp()
        {
            return HttpContext.Connection.RemoteIpAddress;
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public LoginUser CurrentUser
        {
            get
            {
                var claimIdentity = User.Identity as ClaimsIdentity;
                var user = new LoginUser();
                foreach (var claim in claimIdentity.Claims)
                {
                    switch (claim.Type)
                    {
                        case "userNo":
                            user.UserNo = claim.Value;
                            break;
                        case ClaimTypes.Name:
                            user.UserName = claim.Value;
                            break;
                        case "isAdmin":
                            user.IsAdmin = Convert.ToBoolean(claim.Value);
                            break;
                        default:
                            break;
                    }
                }
                return user;
            }
        }

        /// <summary>
        /// 检测图形验证码是否匹配
        /// </summary>
        /// <param name="valcode"></param>
        protected void MatchAuthCode(string valcode)
        {

            if (valcode == null)
            {
                valcode = "";
            }
            var nowval = HttpContext.Session.GetString("valcode");
            if (nowval == null)
            {
                nowval = "";
            }
            if (nowval.ToUpper() != valcode.ToUpper())
            {
                throw new Exception("验证码错误。");
            }
        }
    }
}