using Lstech.Common.Data;
using Lstech.Models.Frame;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.IFrameManager
{
    public interface IModuleManager
    {
        /// <summary>
        /// 获取全部模块列表
        /// </summary>
        /// <returns></returns>
        Task<ListResult<SysModuleModel>> GetModluleAllAsync();
    }
}
