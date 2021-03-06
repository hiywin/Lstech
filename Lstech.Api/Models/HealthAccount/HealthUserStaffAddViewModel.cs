﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lstech.Api.Models.HealthAccount
{
    public class HealthUserStaffAddViewModel
    {
        /// <summary>
        /// 上级工号
        /// </summary>
        [Required]
        public string UpStaffNo { get; set; }
        [Required]
        public List<string> LstStaffNo { get; set; }
    }
}
