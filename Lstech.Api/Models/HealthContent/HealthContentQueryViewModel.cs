using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthContent
{
    public class HealthContentQueryViewModel
    {
        public string Answer { get; set; }
        public string Creator { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
