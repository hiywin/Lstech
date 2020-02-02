using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class ErrData<T>
    {
        public string Msg { get; private set; }

        public int Code { get; set; }

        public bool HasErr { get; private set; }

        public T Data { get; set; }

        public double ExpandSeconds { get; set; }

        public void SetInfo(string err = "", int code = 0)
        {
            this.Msg = err;
            this.Code = code;
            this.HasErr = code < 0;
        }

        public void SetInfo(T obj, string err = "", int code = 0)
        {
            this.Data = obj;
            this.Msg = err;
            this.Code = code;
            this.HasErr = code < 0;
        }
    }
}
