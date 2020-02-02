using Lstech.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthTitle
{
    public class HealthTitleQueryViewModel
    {
        public string Content { get; set; }
        public string Creator { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
