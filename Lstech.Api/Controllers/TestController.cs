using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.IFrameManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class TestController : BaseController
    {
        private readonly IModuleManager _manager;
        public TestController(IModuleManager manager)
        {
            _manager = manager;
        }

        [HttpGet, Route("get_modules")]
        public async Task<IActionResult> GetModules()
        {
            var result = await _manager.GetModluleAllAsync();

            return Ok(result);
        }

        [HttpGet, Route("get_something")]
        public async Task<IActionResult> GetSomeThing()
        {
            var res = await Task.Run<string>(() =>
            {
                return "测试";
            });

            return Ok(res);
        }
    }
}