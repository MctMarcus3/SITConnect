using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NETCore.MailKit.Core;
using SITConnect.Model;

namespace SITConnect.Pages.Account
{
    public class TwoFactorModel : PageModel
    {
        private UserManager<ApplicationUser> _userManager;
        private IEmailService _emailService;

        [BindProperty]
        public TwoFactor TFAModel { get; set; }

        public TwoFactorModel(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            this._userManager = userManager;
            this._emailService = emailService;
        }

        public async Task OnGetAsync(string email, string rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var securityCode = _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await _emailService.SendAsync(email, "Your OTP", $"Use this code as OTP: {securityCode}");
        }
    }
}
