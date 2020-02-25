using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using A2.TwitterClone.UI.Model;
using A2.TwitterClone.UI.model;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace A2.TwitterClone.UI
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        
        public string ConfirmPassword { get; set; }
        [BindProperty]
        [Required]
        [StringLength(8, ErrorMessage = "The userName should be minimum of 6 chars and max of 8", MinimumLength = 6)]
         public string UserName { get; set; }
        [BindProperty]
        [RegularExpression(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)",ErrorMessage ="Inavlid mobile number")]
        [Required]
        public string MobileNumber { get; set; }
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ILogger logger;
        public RegisterModel(UserManager<ApplicationUser> appUserManager,
            SignInManager<ApplicationUser> appSigninManager,
            ILogger<RegisterModel> logger)
        {
            this.userManager = appUserManager;
            this.signInManager = appSigninManager;
            this.logger = logger;
        }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Password.CompareTo(ConfirmPassword) == 0)
                {
                    var user = await userManager.FindByEmailAsync(Email) ??
                        await userManager.FindByNameAsync(UserName);

                    if (user == null)
                    {
                        var applicationUser = new ApplicationUser
                        {
                            Email = this.Email,
                            MobileNumber = this.MobileNumber,
                            UserName = this.UserName,
                        };
                        var result = await userManager.CreateAsync(applicationUser, this.Password);
                        
                        if (!result.Succeeded)
                            ModelState.AddModelError("UserCreateionFailed", "Unable to create user contact admin");
                        else
                        {
                            logger.LogInformation("User created a new account with password.");
                            var signInResult = signInManager.PasswordSignInAsync(applicationUser,
                                Password, false, false);
                            
                            logger.LogInformation("SignOn Complete");
                            return Redirect("/SecuredPage/Tweet");

                        }

                    }
                    else
                        ModelState.AddModelError("UserAlreadyExists", "User is already registered");
                }
                else
                {
                    ModelState.AddModelError("InvalidPasssword", "Characters provided in Password and confirm password does not match");
                }
                
            }
            return Page();
            //return LocalRedirect("/Register");
        }
    }
}