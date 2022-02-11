using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SITConnect.Model;

namespace SITConnect.Pages.Account
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly Captcha _captcha;

        [BindProperty]
        public Login LoginViewModel { get; set; }

        public IndexModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostEnvironment, Captcha captcha)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
            this._captcha = captcha;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid) return Page();
            if (!Request.Form.ContainsKey("g-recaptcha-response")) return Page();
            var captcha = Request.Form["g-recaptcha-response"].ToString();
            if (!await _captcha.IsValid(captcha)) return Page();

            var result = await signInManager.PasswordSignInAsync(
                this.LoginViewModel.Email, 
                this.LoginViewModel.Password, 
                this.LoginViewModel.RememberMe, 
                true);

            if (result.RequiresTwoFactor) return RedirectToPage("/Accounts/TwoFactor",
               new
               {
                   Email = this.LoginViewModel.Email,
                   RememberMe = this.LoginViewModel.RememberMe
               });
            if (result.Succeeded) return RedirectToPage("/Index");
           
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("Login", "Account is Locked out!");
            }
            else
            {
                ModelState.AddModelError("Login", "Failed to Login!");
            }

            return Page();
        }
    }
}
