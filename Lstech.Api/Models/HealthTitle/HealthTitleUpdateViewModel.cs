using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthTitle
{
    public class HealthTitleUpdateViewModel
    {
        [Required]
        public string TitleId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Type { get; set; }
        public bool? IsMustFill { get; set; }
        public int Sort { get; set; }
        public bool? IsShow { get; set; }
    }
}
