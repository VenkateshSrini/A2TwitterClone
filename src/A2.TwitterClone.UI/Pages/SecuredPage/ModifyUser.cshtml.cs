using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using A2.TwitterClone.UI.Model;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class ModifyUserModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string MobileNumber { get; set; }
        [BindProperty]
        [Required]
        [StringLength(8, ErrorMessage = "The userName should be minimum of 6 chars and max of 8", MinimumLength = 6)]
        public string UserName { get; set; }
        
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ILogger logger;
        public ModifyUserModel(UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> appSignInManager, ILogger<ModifyUserModel> logger)
        {
            userManager = appUserManager;
            signInManager = appSignInManager;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                ModelState.AddModelError("UserNotFound", "User does not exist");
            else
            {
                
                Email = user.Email;
                MobileNumber = user.MobileNumber;
                UserName = user.UserName;
            }
            return Page();
        }
        public async Task<IActionResult>OnPost()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                ModelState.AddModelError("UserNotFound", "User does not exist");
            else
            {
                var checkUser = await userManager.FindByNameAsync(UserName);
                if (checkUser == null)
                {
                    user.UserName = UserName;
                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        await signInManager.RefreshSignInAsync(user);
                        return LocalRedirect("/SecuredPage/Tweet");
                    }
                    else
                        ModelState.AddModelError("UserUpdateFailed", "Unable to update user");
                }
                else
                    ModelState.AddModelError("UserUpdateFailed", "User by that name already exists");


            }
            return Page();
        }
    }
}