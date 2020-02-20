using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace A2.TwitterClone.UI
{
    public class RegisterAndLoginModel : PageModel
    {
        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [DataType(DataType.Password)]
        [BindProperty]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        [Required]
        [StringLength(8, ErrorMessage = "The userName should be minimum of 6 chars and max of 8", MinimumLength = 6)]
         public string UserName { get; set; }
        public void OnGet()
        {
            
        }
    }
}