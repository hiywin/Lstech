using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthTitle
{
    public class HealthTitleDeleteViewModel
    {
        [Required]
        public string TitleId { get; set; }
    }
}
