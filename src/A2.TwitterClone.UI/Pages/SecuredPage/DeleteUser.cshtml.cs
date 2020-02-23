﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.TwitterClone.UI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class DeleteUserModel : PageModel
    {
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        private readonly ILogger<DeleteUserModel> logger;
        public DeleteUserModel(SignInManager<ApplicationUser> appSignInManager,
           UserManager<ApplicationUser> appUserManager, ILogger<DeleteUserModel> logger)
        {
            signInManager = appSignInManager;
            userManager = appUserManager;
            this.logger = logger;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var user = await userManager.GetUserAsync(User);
            if (user==null)
            {
                ModelState.AddModelError("UserNotFound", "User does not exist");
            }
            else
            {
                var results = await userManager.DeleteAsync(user);
                if (results.Succeeded)
                {
                    await signInManager.SignOutAsync();
                    return Redirect("/Register");
                }
            }
            return Page();
            //var user = userManager.FindByIdAsync(User.Identities)
        }
    }
}