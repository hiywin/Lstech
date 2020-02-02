using Lstech.Entities.Base;
using Lstech.IBaseManager;
using Lstech.Utility.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lstech.BaseManager
{
    public class InitialManager : IInitialManager
    {
        public bool InitData(ISqlConnModel model)
        {
            return InitOperators.Init(model);
        }
    }
}
