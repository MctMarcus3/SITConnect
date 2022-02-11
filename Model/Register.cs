using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static SITConnect.Model.CustomValidators;

namespace SITConnect.Model
{
    public class Register 
    {
        public static string[] ValidImageMimes { get; private set; } = 
            { "image/jpeg", "image/gif", "image/png", "image/webp"};

        [Required]
        [Display(Name = "First Name")]
        [PersonalData]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [PersonalData]
        public string LastName { get; set; }

        [CreditCard]
        [Display(Name = "Credit Card Number")]
        [PersonalData]
        [Required]
        public string CreditCard { get; set; }

        [EmailAddress]
        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [ValidateDateRange(ErrorMessage = "Invalid Date!")]
        [PersonalData]
        public DateTime? DateofBirth { get; set; }
        
        //[Required(ErrorMessage = "Please select file.")]
        [MaxFileSize(5*1024 * 1024, ErrorMessage = "File must be less than or equal to 5mb.")]
        [ValidateImage(validImageMimes: new string[]{"image/jpeg", "image/gif", "image/png", "image/webp"})]
        [Display(Name = "Profile Photo")]
        //[Required("")]
        public IFormFile Photo { get; set; }
    }
}
