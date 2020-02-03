using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lstech.Api.Models.HealthContent;
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
    public class HealthContentController : BaseController
    {
        private readonly IHealthContentManager _manager;
        public HealthContentController(IHealthContentManager manager)
        {
            _manager = manager;
        }

        //[Authorize, HttpPost, Route("get_health_contents")]
        //public async Task<IActionResult> GetHealthContents(HealthContentQueryViewModel model)
        //{
        //    var query = new QueryData<HealthContentQuery>()
        //    {
        //        Criteria = new HealthContentQuery()
        //        {
        //            Answer = model.Answer,
        //            Creator = model.Creator
        //        },
        //        PageModel = model.PageModel
        //    };
        //    var result = await _manager.GetHealthContentPageAsync(query);

        //    return Ok(result);
        //}

        [Authorize, HttpPost, Route("get_health_contents")]
        public async Task<IActionResult> GetHealthContents(HealthContentQueryViewModel model)
        {
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    Answer = model.Answer,
                    Creator = model.Creator
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetHealthContentPageAsyncEx(query);

            return Ok(result);
        }

        [HttpGet, Route("health_content_export")]
        public async Task<IActionResult> HealthContentExport(int pageIndex,int pageSize)
        {
            //var query = new QueryData<HealthContentQuery>()
            //{
            //    Criteria = new HealthContentQuery()
            //    {
            //        Answer = model.Answer,
            //        Creator = model.Creator
            //    },
            //    PageModel = model.PageModel
            //};
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    Answer = string.Empty,
                    Creator = string.Empty
                },
                PageModel = new PageModel()
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize
                }
            };
            var result = await _manager.HealthContentExcelExportAsync(query);
            if (result.HasErr)
            {
                return Ok(result);
            }

            return File(result.Data, "application/ms-excel", $"{Guid.NewGuid().ToString()}.xlsx");
        }
    }
}