using Lstech.Common.Data;
using Lstech.IWCFService.Structs;
using Lstech.Mobile.IHealthManager;
using Lstech.Models.Health;
using Lstech.Utility;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lstech.Mobile.HealthManager
{
    public class HealthAccountMobileManager : IHealthAccountMobileManager
    {
        public async Task<ErrData<DDUserInfoModel>> GetDDUserInfoAsync(QueryData<WebServiceDDQuery> query)
        {
            var result = new ErrData<DDUserInfoModel>();
            var dt = DateTime.Now;

            var res = await HealthMobileOperaters.TlgChinaWebServiceOperater.GetDDUserInfoAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
            }
            else
            {
                if (string.IsNullOrEmpty(res.Data))
                {
                    result.SetInfo("获取钉钉用户信息失败", -102);
                }
                else
                {
                    var userInfoVar = new DDUserInfoModel();
                    string json = ReplaceString(res.Data);
                    string[] userArr = json.Split(',');
                    for (int i = 0; i < userArr.Length; i++)
                    {
                        if (userArr[i].ToString().Contains("userid"))
                        {
                            string[] valueId = userArr[i].ToString().Split(':');
                            userInfoVar.useeid = valueId[1].ToString();
                        }
                        if (userArr[i].ToString().Contains("jobnumber"))
                        {
                            string[] valueJobnumber = userArr[i].ToString().Split(':');
                            userInfoVar.jobnumber = valueJobnumber[1].ToString();
                        }
                        if (userArr[i].ToString().Contains("name"))
                        {
                            string[] valueName = userArr[i].ToString().Split(':');
                            userInfoVar.name = valueName[1].ToString();
                        }
                    }

                    result.Data = userInfoVar;
                    result.SetInfo(userInfoVar, "获取成功", 200);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }


        public static string ReplaceString(string JsonString)
        {
            if (JsonString == null) { return JsonString; }
            if (JsonString.Contains("\\"))
            {
                JsonString = JsonString.Replace("\\", "");
            }
            if (JsonString.Contains("\\\""))
            {
                JsonString = JsonString.Replace("\\", "");
            }
            if (JsonString.Contains("\'"))
            {
                JsonString = JsonString.Replace("\'", "\\\'");
            }
            if (JsonString.Contains("\""))
            {
                JsonString = JsonString.Replace("\"", "");
            }
            //去掉字符串的回车换行符
            JsonString = Regex.Replace(JsonString, @"[\n\r]", "");
            JsonString = JsonString.Trim();
            return JsonString;
        }

    }
}
