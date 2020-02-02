using Lstech.Common.Data;
using Lstech.IFrameManager;
using Lstech.IFrameService.Structs;
using Lstech.Models.Frame;
using Lstech.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.FrameManager
{
    public class ModuleManager : IModuleManager
    {
        public async Task<ListResult<SysModuleModel>> GetModluleAllAsync()
        {
            var lr = new ListResult<SysModuleModel>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<SysModuleQuery>()
            {
                Criteria = new SysModuleQuery()
                {
                    IsDelete = false
                }
            };
            var res = await FrameOperaters.ModuleOperater.GetModulesAllAsync(queryEx);
            if (res.HasErr)
            {
                lr.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                foreach (var item in res.Data)
                {
                    var info = new SysModuleModel();
                    info.ModuleNo = item.ModuleNo;
                    info.ModuleName = item.ModuleName;
                    info.ParentNo = item.ParentNo;
                    info.Icon = item.Icon;
                    info.Url = item.Url;
                    info.Category = item.Category;
                    info.Target = item.Target;
                    info.IsResource = item.IsResource;
                    info.App = item.App;
                    info.Sort = item.Sort;
                    lr.Results.Add(info);
                }
                lr.SetInfo("成功", 200);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }
    }
}
