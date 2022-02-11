using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace SITConnect.Model
{
    public class Login 
    {
        //[RegularExpression(@"/^\w+[\+\.\w-]*@([\w-]+\.)*\w+[\w-]*\.([a-z]|\d+)$/i", ErrorMessage = "Invalid Email address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
