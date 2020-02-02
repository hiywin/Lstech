using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models;
using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Models.Health;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> InsertHealthContentInfo(AddHealthContentModel model)
        {
            var condition = new Health_content_Model();
            condition.TitleId = model.TitleId;
            condition.Answer = model.Answer;
            condition.ContentId = model.ContentId;
            condition.CreateTime = model.CreateTime;
            condition.Creator = model.Creator;
            condition.TitleType = model.TitleType;
            condition.CreateName = model.CreateName;

            var query = new QueryData<Health_content_Model>();
            query.Criteria = condition;

            var result = await _manager.InsertHealthContentMaAsync(query);

            return Ok(result);
        }
    }
}