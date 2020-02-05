using Lstech.Common.Data;
using Lstech.IWCFService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.IWCFService
{
    public interface ITlgChinaWebService
    {
        /// <summary>
        /// 获取用户AD的GUID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<string>> GetUserADGuidAsync(QueryData<WcfADUserGuidQuery> query);


        Task<DataResult<string>> GetDDUserInfoAsync(QueryData<WebServiceDDQuery> query);
    }
}
