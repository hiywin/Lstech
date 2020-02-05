using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Models.Health
{
    public class DDUserInfoJsonModel
    {
        //{\\\"errcode\\\":0,\\\"unionid\\\":\\\"vPZiSZZ6W05DhK3O3jCHuPwiEiE\\\",\\\"openId\\\":\\\"vPZiSZZ6W05DhK3O3jCHuPwiEiE\\\",\\\"remark\\\":\\\"\\\",\\\"userid\\\":\\\"6019722\\\",\\\"isLeaderInDepts\\\":\\\"{131358561:false}\\\",\\\"isBoss\\\":false,\\\"isSenior\\\":false,\\\"tel\\\":\\\"131545\\\",\\\"department\\\":[131358561],\\\"workPlace\\\":\\\"\\\",\\\"orderInDepts\\\":\\\"{131358561:176350138413469512}\\\",\\\"dingId\\\":\\\"$:LWCP_v1:$RmK7+2BjcFNVlmMdRl4YfQ==\\\",\\\"mobile\\\":\\\"18975548819\\\",\\\"errmsg\\\":\\\"ok\\\",\\\"active\\\":true,\\\"avatar\\\":\\\"https://static-legacy.dingtalk.com/media/lADPDgQ9rJNtnX3NA8DNA8A_960_960.jpg\\\",\\\"isAdmin\\\":false,\\\"tags\\\":{},\\\"isHide\\\":false,\\\"jobnumber\\\":\\\"6019722\\\",\\\"name\\\":\\\"曾涛 Andre Zeng\\\",\\\"stateCode\\\":\\\"86\\\",\\\"position\\\":\\\"工程师\\\"}
        public string errcode { get; set; }
        public string unionid { get; set; }
        public string openId { get; set; }
        public string remark { get; set; }
        public string userid { get; set; }
        public List<string> isLeaderInDepts { get; set; }
        public bool isBoss { get; set; }
        public bool isSenior { get; set; }
        public string tel { get; set; }
        public Array[] department { get; set; }
        public string workPlace { get; set; }
        public string orderInDepts { get; set; }
        public string dingId { get; set; }
        public string mobile { get; set; }
        public string errmsg { get; set; }
        public bool active { get; set; }
        public string avatar { get; set; }
        public bool isAdmin { get; set; }
        public List<string> tags { get; set; }
        public bool isHide { get; set; }
        public string jobnumber { get; set; }
        public string name { get; set; }
        public string stateCode { get; set; }
        public string position { get; set; }

    }
}
