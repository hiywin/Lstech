using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.Common.Data
{
    public class PageModel
    {
        public PageModel()
        {
            _pageIndex = 1;
            _pageSize = 20;
        }

        private int _pageIndex;
        private int _pageSize;
        private int _totalCount;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        public int PageTotal
        {
            get
            {
                if (_totalCount == 0 || _pageSize == 0) return 0;
                return (_totalCount / _pageSize) + (_totalCount % _pageSize == 0 ? 0 : 1);
            }
        }
    }
}
