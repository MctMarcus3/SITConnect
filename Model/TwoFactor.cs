using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SITConnect.Model
{
    public class TwoFactor
    {
        [Required]
        [Display(Name = "Verification Code")]
        [PersonalData]
        public string SecurityCode { get; set; }        
        
        [PersonalData]
        public bool RememberMe{ get; set; }
    }
}
