using Lstech.Common.Data;
using Lstech.Common.Wcf;
using Lstech.Entities.WCF;
using Lstech.IWCFService;
using Lstech.IWCFService.Structs;
using Lstech.Models.WCF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TlgChinaService;

namespace Lstech.WCFService
{
    public class TlgChinaWebService : ITlgChinaWebService
    {
        public async Task<DataResult<string>> GetUserADGuidAsync(QueryData<WcfADUserGuidQuery> query)
        {
            var result = new DataResult<string>();
            try
            {
                using (var chnl = WcfInvoke.CreateWCFChannel<lytechWebServiceSoap>(WcfInvoke.TlgChinaServiceUrl))
                {
                    var proxy = chnl.CreateChannel();
                    result.Data = await proxy.GetUserADGUIDAsync(query.Criteria.UserName, query.Criteria.Password);
                }

                //var binding = new BasicHttpBinding();
                ////根据 WebService 的 URL 构建终端点对象
                //var endpoint = new EndpointAddress(@"http://ws.tlgchina.com:5678/lytechWebService.asmx");
                ////创建调用接口的工厂，注意这里泛型只能传入接口
                //var factory = new ChannelFactory<lytechWebServiceSoap>(binding, endpoint);
                ////从工厂获取具体的调用实例
                //var callClient = factory.CreateChannel();
                ////调用具体的方法，这里是 HelloWorldAsync 方法
                //result.Data = await callClient.GetUserADGUIDAsync(query.Criteria.UserName, query.Criteria.Password);
            }
            catch (Exception ex)
            {
                result.SetErr(ex, -500);
                result.Data = string.Empty;
            }

            return result;
        }

        public async Task<DataResult<string>> GetDDUserInfoAsync(QueryData<WebServiceDDQuery> query)
        {
            var result = new DataResult<string>();
            try
            {
                using (var chnl = WcfInvoke.CreateWCFChannel<lytechWebServiceSoap>(WcfInvoke.TlgChinaServiceUrl))
                {
                    var proxy = chnl.CreateChannel();
                    result.Data = await proxy.GetUserInfoAndLastApprAsync(query.Criteria.code, query.Criteria.ProcessCode, query.Criteria.corpid);
                }
            }
            catch (Exception ex)
            {
                result.SetErr(ex, -500);
                result.Data = string.Empty;
            }

            return result;
        }

        public async Task<DataResult<IADUserInfo>> GetADUserInfoAsync(QueryData<WcfADUserInfoQuery> query)
        {
            var result = new DataResult<IADUserInfo>();
            try
            {
                using (var chnl = WcfInvoke.CreateWCFChannel<lytechWebServiceSoap>(WcfInvoke.TlgChinaServiceUrl))
                {
                    var proxy = chnl.CreateChannel();
                    var res = await proxy.GetADuserAsync(query.Criteria.UserName, query.Criteria.Password);
                    if (!string.IsNullOrEmpty(res))
                    {
                        var users = res.Split('|');
                        IADUserInfo info = new ADUserInfo();
                        info.UserNo = users[0];
                        info.UserName = users[1];
                        info.ADAccount = users[2];

                        result.Data = info;
                    }
                }
            }
            catch (Exception ex)
            {
                result.SetErr(ex, -500);
                result.Data = null;
            }

            return result;
        }

    }
}
