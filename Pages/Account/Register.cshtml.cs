using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SITConnect.Model;

namespace SITConnect.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostEnvironment;

        [BindProperty]
        public Register model { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            
            if (model.Photo != null)
            {
                string filePath = Path.Combine(hostEnvironment.WebRootPath, "img/profile");
                
                model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreditCard = model.CreditCard,
                Email = model.Email,
                UserName = model.Email,
                DateofBirth = model.DateofBirth,
                PhotoPath = model.Photo
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("Account/Login");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Page();
        }
    }
}
