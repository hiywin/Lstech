using Lstech.Common.Data;
using Lstech.IWCFService.Structs;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    public interface IHealthAccountMobileManager
    {
        Task<ErrData<DDUserInfoModel>> GetDDUserInfoAsync(QueryData<WebServiceDDQuery> query);
    }
}
