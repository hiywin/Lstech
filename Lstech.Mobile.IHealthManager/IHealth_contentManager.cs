using Lstech.Common.Data;
using Lstech.Models.Health;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lstech.Mobile.IHealthManager
{
    public interface IHealth_contentManager
    {
        Task<ErrData<bool>> InsertHealthContentMaAsync(QueryData<Health_content_Model> query);
    }
}
