using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models.HealthTitle;
using Lstech.Common.Data;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lstech.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class HealthTitleController : BaseController
    {
        private readonly IHealthTitleManager _manager;
        public HealthTitleController(IHealthTitleManager manager)
        {
            _manager = manager;
        }

        [Authorize, HttpPost, Route("get_health_titles")]
        public async Task<IActionResult> GetHealthTitles(HealthTitleQueryViewModel model)
        {
            var query = new QueryData<HealthTitleQuery>()
            {
                Criteria = new HealthTitleQuery()
                {
                    Content = model.Content,
                    Creator = model.Creator
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetHealthTitlePageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("health_title_save")]
        public async Task<IActionResult> HealthTitleSave(HealthTitleSaveViewModel model)
        {
            var query = new QueryData<HealthTitleSaveQuery>()
            {
                Criteria = new HealthTitleSaveQuery()
                {
                    Content = model.Content,
                    Type = model.Type,
                    IsMustFill = model.IsMustFill,
                    ParentId = model.ParentId,
                    Sort = model.Sort,
                    IsShow = model.IsShow
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo
                }
            };
            var result = await _manager.HealthTitleSaveAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("health_title_update")]
        public async Task<IActionResult> HealthTitleUpdate(HealthTitleUpdateViewModel model)
        {
            var query = new QueryData<HealthTitleUpdateQuery>()
            {
                Criteria = new HealthTitleUpdateQuery()
                {
                    TitleId = model.TitleId,
                    Content = model.Content,
                    Type = model.Type,
                    IsMustFill = model.IsMustFill,
                    Sort = model.Sort,
                    IsShow = model.IsShow
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo
                }
            };
            var result = await _manager.HealthTitleUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("health_title_delete")]
        public async Task<IActionResult> HealthTitleDelete(HealthTitleDeleteViewModel model)
        {
            var query = new QueryData<HealthTitleDeleteQuery>()
            {
                Criteria = new HealthTitleDeleteQuery()
                {
                    TitleId = model.TitleId
                }
            };
            var result = await _manager.HealthTitleDeleteAsync(query);

            return Ok(result);
        }
    }
}