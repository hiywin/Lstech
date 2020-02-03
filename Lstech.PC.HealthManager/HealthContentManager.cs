using Lstech.Common.Data;
using Lstech.Entities.Health;
using Lstech.PC.IHealthManager;
using Lstech.PC.IHealthService.Structs;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.PC.HealthManager
{
    public class HealthContentManager : IHealthContentManager
    {
        public async Task<ListResult<IHealthContent>> GetHealthContentPageAsync(QueryData<HealthContentQuery> query)
        {
            var lr = new ListResult<IHealthContent>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthContentOperater.GetHealthContentPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    lr.Results.Add(item);
                }
                lr.SetInfo("成功", 200);
                lr.PageModel = res.PageInfo;
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        public async Task<ListResult<DataTable>> GetHealthContentPageAsyncEx(QueryData<HealthContentQuery> query)
        {
            var lr = new ListResult<DataTable>();
            var dt = DateTime.Now;

            var res = await HealthPcOperaters.HealthContentOperater.GetHealthContentPageAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                var lstTitle = new List<string>();
                #region 获取标题
                var queryTitle = new QueryData<HealthTitleQuery>()
                {
                    Criteria = new HealthTitleQuery()
                    {
                        ParentId = string.Empty,
                        IsParentQuery = true,
                        IsShow = true
                    }
                };
                var resTitle = await HealthPcOperaters.HealthTitleOperater.GetHealthTitleAllAsync(queryTitle);
                if (resTitle.HasErr)
                {
                    lr.SetInfo(resTitle.ErrMsg, resTitle.ErrCode);
                    lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return lr;
                }
                else
                {
                    foreach (var title in resTitle.Data)
                    {
                        if (!lstTitle.Contains(title.Content))
                        {
                            lstTitle.Add(title.Content);
                        }
                    }
                }
                #endregion
                #region 创建动态Table
                
                var table = new DataTable();
                table.Columns.Add("工号", Type.GetType("System.String"));
                table.Columns.Add("姓名", Type.GetType("System.String"));
                table.Columns.Add("时间", Type.GetType("System.DateTime"));
                foreach (var column in lstTitle)
                {
                    table.Columns.Add(column, Type.GetType("System.String"));
                }

                #endregion
                #region 绑定数据

                foreach (var item in res.Data)
                {
                    DataRow row = table.NewRow();
                    row["工号"] = item.Creator;
                    row["姓名"] = item.CreateName;
                    row["时间"] = item.CreateTime;

                    var answer = item.Answer;
                    var anArray = answer.Split(';');
                    if (anArray.Length > 0)
                    {
                        foreach (var tem in anArray)
                        {
                            var temArray = tem.Split(':');
                            if (table.Columns.Contains(temArray[0]))
                            {
                                row[temArray[0]] = temArray[1];
                            }
                        }
                    }
                    table.Rows.Add(row);
                }
                #endregion

                lr.Results.Add(table);
                lr.SetInfo("查询成功", 200);
                lr.PageModel = res.PageInfo;
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
