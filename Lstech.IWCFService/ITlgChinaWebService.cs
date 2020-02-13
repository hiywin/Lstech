using Lstech.Common.Data;
using Lstech.Entities.WCF;
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

        /// <summary>
        /// 获取DD用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<string>> GetDDUserInfoAsync(QueryData<WebServiceDDQuery> query);

        /// <summary>
        /// 根据用户AD获取用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<IADUserInfo>> GetADUserInfoAsync(QueryData<WcfADUserInfoQuery> query);
    }
}
