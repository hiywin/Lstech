using Lstech.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.IBaseManager
{
    public interface IInitialManager
    {
        bool InitData(ISqlConnModel model);
    }
}
