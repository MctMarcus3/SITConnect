using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static SITConnect.Model.CustomValidators;

namespace SITConnect.Model
{
    public class Register 
    {
        [Required]
        [Display(Name = "First Name")]
        [PersonalData]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        [PersonalData]
        public string LastName { get; set; }

        [DataType(DataType.CreditCard)]
        [Display(Name = "Credit Card Number")]
        [PersonalData]
        public string CreditCard { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [ValidateDateRange]
        [PersonalData]
        public DateTime? DateofBirth { get; set; }
        
        //[Required(ErrorMessage = "Please select file.")]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.jpeg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public IFormFile Photo { get; set; }
    }
}
