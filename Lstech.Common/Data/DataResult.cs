using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class DataResult<T>
    {
        public DataResult()
        {
            PageInfo = new PageModel();
        }

        /// <summary>
        /// 返回状态码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool HasErr { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PageModel PageInfo { get; set; }

        public void SetErr(Exception ex, int code = 0)
        {
            this.Data = default(T);
            this.ErrCode = code;
            this.ErrMsg = string.Format("{0}", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            this.HasErr = code < 0;
        }

        public void SetErr(string err = "", int code = 0)
        {
            this.ErrMsg = err;
            this.ErrCode = code;
            this.HasErr = code < 0;
        }

        public void SetErr(T obj, string err = "", int code = 0)
        {
            this.Data = obj;
            this.ErrMsg = err;
            this.ErrCode = code;
            this.HasErr = code < 0;
        }
    }
}
