using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using A2.TwitterClone.UI.Model;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string UserName { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ILogger logger;
        private readonly string regexEmailText = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public LoginModel(UserManager<ApplicationUser> appUserManager,
         SignInManager<ApplicationUser> appSignInManager, ILogger<LoginModel> loginLogger)
        {
            userManager = appUserManager;
            signInManager = appSignInManager;
            logger = loginLogger;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            ApplicationUser currentUser=null;
            if (ModelState.IsValid)
            {
                
                currentUser = Regex.IsMatch(UserName, regexEmailText,RegexOptions.IgnoreCase) ?
                               await userManager.FindByEmailAsync(UserName) :
                               await userManager.FindByNameAsync(UserName);
                if (currentUser==null)
                {
                    ModelState.AddModelError("UserDoesnotExist", "User does not exists");
                }
                else
                {
                    var signInResult = await signInManager.PasswordSignInAsync(currentUser,
                                Password,false, false);
                    if (!signInResult.Succeeded)
                    {
                        logger.LogInformation("SignOn failed");
                        ModelState.AddModelError("Invalid User credential", "In valid credential");
                    }
                    else
                    {
                        logger.LogInformation("SignOn Complete");
                        return Redirect("/SecuredPage/Tweet");
                    }

                }
            }
            return Page();
        }
    }
}