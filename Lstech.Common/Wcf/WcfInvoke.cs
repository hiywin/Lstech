using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Lstech.Common.Wcf
{
    public class WcfInvoke
    {
        #region URLs

        public static string SapEnviorn { get; set; }

        public static string TlgChinaServiceUrl { get; set; }

        public static string LstechServiceUrl { get; set; }

        #endregion

        #region Wcf服务工厂

        /// <summary>
        /// 创建WCF通道
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="binding"></param>
        /// <returns></returns>
        public static ChannelFactory<T> CreateWCFChannel<T>(string url, string binding = "basichttpbinding")
        {
            var endpoint = new EndpointAddress(url);
            return new ChannelFactory<T>(CreateBinding(binding), endpoint);
        }

        #endregion

        #region 创建传输协议
        /// <summary>
        /// 创建传输协议
        /// </summary>
        /// <param name="binding">传输协议名称</param>
        /// <returns></returns>
        private static Binding CreateBinding(string binding)
        {
            Binding bindinginstance = null;
            if (binding.ToLower() == "basichttpbinding")
            {
                BasicHttpBinding ws = new BasicHttpBinding();
                ws.MaxBufferSize = 2147483647;
                ws.MaxBufferPoolSize = 2147483647;
                ws.MaxReceivedMessageSize = 2147483647;
                ws.ReaderQuotas.MaxStringContentLength = 2147483647;
                ws.CloseTimeout = new TimeSpan(0, 10, 0);
                ws.OpenTimeout = new TimeSpan(0, 10, 0);
                ws.ReceiveTimeout = new TimeSpan(0, 10, 0);
                ws.SendTimeout = new TimeSpan(0, 10, 0);
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "nettcpbinding")
            {
                NetTcpBinding ws = new NetTcpBinding();
                ws.MaxReceivedMessageSize = 65535000;
                ws.MaxBufferSize = 2147483647;
                ws.MaxBufferPoolSize = 2147483647;
                ws.ReaderQuotas.MaxStringContentLength = 2147483647;
                ws.Security.Mode = SecurityMode.None;
                ws.ReceiveTimeout = new TimeSpan(0, 10, 0);
                ws.SendTimeout = new TimeSpan(0, 10, 0);
                bindinginstance = ws;
            }
            return bindinginstance;

        }
        #endregion
    }
}
