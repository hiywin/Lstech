using Lstech.Common.Data;
using Lstech.Mobile.IHealthManager;
using Lstech.Mobile.IHealthService.Structs;
using Lstech.Models.Health;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    public class Health_titleManager : IHealth_titleManager
    {
        /// <summary>
        /// 获取所有体检内容表头
        /// </summary>
        /// <returns></returns>
        public async Task<ListResult<Health_title_List_Model>> GetHealthTitleAllAsync()
        {
            var lr = new ListResult<Health_title_List_Model>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<GetAllHealthTitleQuery>()
            {
                Criteria = new GetAllHealthTitleQuery()
                {
                    IsShow = true
                }
            };
            var res = await HealthMobileOperaters.HealthTitleOperater.GetAllHealthTitle(queryEx);  ///获取所有体检内容表头
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    var info = new Health_title_List_Model();
                    info.Id = item.Id;
                    info.IsMustFill = item.IsMustFill;
                    info.IsShow = item.IsShow;
                    info.ParentId = item.ParentId;
                    info.Sort = item.Sort;
                    info.TitleId = item.TitleId;
                    info.Type = item.Type;
                    info.UpdateTime = item.UpdateTime;
                    info.Updator = item.Updator;
                    info.Content = item.Content;
                    info.CreateTime = item.CreateTime;
                    info.Creator = item.Creator;
                    var querySub = new QueryData<GetSubHealthTitleQuery>()
                    {
                        Criteria = new GetSubHealthTitleQuery()
                        {
                            IsShow = true,
                            ParentId = item.TitleId
                        }
                    };
                    var resSub = await HealthMobileOperaters.HealthTitleOperater.GetSubHealthTitle(querySub);  //获取所有体检内容表子选项
                    if (resSub.HasErr)
                    {
                        lr.SetInfo(resSub.ErrMsg, resSub.ErrCode);
                    }
                    else
                    {
                        List<Health_title_Model> listTitle = null;
                        if (resSub.Data.Count > 0)
                        {
                            listTitle = new List<Health_title_Model>();
                            foreach (var titles in resSub.Data)
                            {
                                Health_title_Model healthTitle = new Health_title_Model();
                                healthTitle.Content = titles.Content;
                                healthTitle.CreateTime = titles.CreateTime;
                                healthTitle.Creator = titles.Creator;
                                healthTitle.Id = titles.Id;
                                healthTitle.IsMustFill = titles.IsMustFill;
                                healthTitle.IsShow = titles.IsShow;
                                healthTitle.ParentId = titles.ParentId;
                                healthTitle.Sort = titles.Sort;
                                healthTitle.TitleId = titles.TitleId;
                                healthTitle.Type = titles.Type;
                                healthTitle.UpdateTime = titles.UpdateTime;
                                healthTitle.Updator = titles.Updator;
                                listTitle.Add(healthTitle);
                            }
                            info.healthTitleList = listTitle;
                        }
                    }
                    lr.Results.Add(info);
                }
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
