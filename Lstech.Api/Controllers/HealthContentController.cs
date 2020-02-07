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

        /// <summary>
        /// 查询体检内容-使用
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_health_contents")]
        public async Task<IActionResult> GetHealthContents(HealthContentQueryViewModel model)
        {
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    Answer = model.Answer,
                    Creator = model.Creator,
                    CreateName = model.CreateName,
                    StarTime = model.StarTime,
                    EndTime = model.EndTime,
                    HrLeaderNo = CurrentUser.IsAdmin ? string.Empty : CurrentUser.UserNo
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

        [HttpGet, Route("health_content_export_all")]
        public async Task<IActionResult> HealthContentExportAll(DateTime? starTime,DateTime? endTime)
        {
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    StarTime = starTime,
                    EndTime = endTime
                }
            };
            var result = await _manager.HealthContentExcelExportAllAsync(query);
            if (result.HasErr)
            {
                return Ok(result);
            }

            return File(result.Data, "application/ms-excel", $"{Guid.NewGuid().ToString()}.xlsx");
        }

        [Authorize, HttpPost, Route("health_content_export_hr_all")]
        public async Task<IActionResult> HealthContentExportHrAll(HealthContentExportHrViewModel model)
        {
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    StarTime = model.StarTime,
                    EndTime = model.EndTime
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    IsAdmin = CurrentUser.IsAdmin
                }
            };
            var result = await _manager.HealthContentExcelExportHrAllAsync(query);
            if (result.HasErr)
            {
                return Ok(result);
            }

            return File(result.Data, "application/ms-excel", $"{Guid.NewGuid().ToString()}.xlsx");
        }

        /// <summary>
        /// 导出体检内容-使用
        /// </summary>
        /// <param name="userNo"></param>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        [HttpGet, Route("health_content_export_hr_get")]
        public async Task<IActionResult> HealthContentExportHrGet(string userNo, DateTime? starTime, DateTime? endTime)
        {
            var query = new QueryData<HealthContentQuery>()
            {
                Criteria = new HealthContentQuery()
                {
                    StarTime = starTime,
                    EndTime = endTime
                },
                Extend = new QueryExtend()
                {
                    UserNo = userNo,
                    IsAdmin = true
                }
            };
            var result = await _manager.HealthContentExcelExportHrAllAsync(query);
            if (result.HasErr)
            {
                return Ok(result);
            }

            return File(result.Data, "application/ms-excel", $"{Guid.NewGuid().ToString()}.xlsx");
        }
    }
}