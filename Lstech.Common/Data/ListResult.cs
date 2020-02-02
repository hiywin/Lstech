using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class ListResult<T> : ErrData<T>
    {
        public ListResult()
        {
            Results = new List<T>();
        }

        public List<T> Results { get; private set; }

        public PageModel PageModel { get; set; }

        public void SetData(List<T> obj = null, string err = "")
        {
            SetInfo(err);
            if (obj != null) Results = obj;
        }
    }
}
