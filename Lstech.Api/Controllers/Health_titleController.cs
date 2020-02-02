using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Mobile.IHealthManager;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class Health_titleController : BaseController
    {
        private readonly IHealth_titleManager _manager;
        public Health_titleController(IHealth_titleManager manager)
        {
            _manager = manager;
        }

        [HttpGet, Route("get_HealthTitleAll")]
        public async Task<IActionResult> GetHealthTitleAll()
        {
            var result = await _manager.GetHealthTitleAllAsync();

            return Ok(result);
        }
    }
}