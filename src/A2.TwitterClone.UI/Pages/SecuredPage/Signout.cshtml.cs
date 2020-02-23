using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using A2.TwitterClone.UI.Model;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class SignoutModel : PageModel
    {
        private SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<SignoutModel> logger;
        public SignoutModel(SignInManager<ApplicationUser> appSignInManager, 
            ILogger<SignoutModel> logger)
        {
            signInManager = appSignInManager;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet()
        {
            await signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }
    }
}