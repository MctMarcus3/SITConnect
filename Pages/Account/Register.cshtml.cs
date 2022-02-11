using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using SITConnect.Model;

namespace SITConnect.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly Captcha _captcha;
        private readonly IDataProtector _protector;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            IWebHostEnvironment hostEnvironment, 
            Captcha captcha,
            IDataProtectionProvider dataProtectionProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostEnvironment = hostEnvironment;
            this._captcha = captcha;
            this._protector = dataProtectionProvider.CreateProtector("CreditCardInfo");
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
            
            string uniqueFileName = null;
            if (RegisterViewModel.Photo != null)
            {
                uniqueFileName = RegisterViewModel.Email + "_" + DateTimeOffset.Now.ToUnixTimeSeconds() + Path.GetExtension(RegisterViewModel.Photo.FileName);
            }
            
            ApplicationUser user = new()
            {
                FirstName = RegisterViewModel.FirstName,
                LastName = RegisterViewModel.LastName,
                CreditCard = _protector.Protect(RegisterViewModel.CreditCard),
                Email = RegisterViewModel.Email,
                UserName = RegisterViewModel.Email,
                DateofBirth = RegisterViewModel.DateofBirth,
                PhotoPath = uniqueFileName
            };
            var result = await userManager.CreateAsync(user, RegisterViewModel.Password);

            if (result.Succeeded)
            {
                string filePath = Path.Combine(hostEnvironment.WebRootPath, "img/profile", uniqueFileName);
                RegisterViewModel.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                return RedirectToPage("/Account/Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return Page();
        }
    }
}
