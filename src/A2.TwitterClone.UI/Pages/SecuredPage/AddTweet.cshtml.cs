using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using A2.TwitterClone.UI.model;
using A2.TwitterClone.UI.Repository;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class AddTweetModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Tweets { get; set; }
        public const string nameIdentifer = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private readonly ITweetRepo tweetRepo;
        private ILogger<AddTweetModel> logger;
        public AddTweetModel(ITweetRepo tweetRepo, ILogger<AddTweetModel> logger)
        {
            this.tweetRepo = tweetRepo;
            this.logger = logger;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim =>
            claim.Type.CompareTo(nameIdentifer) == 0);
            if (!ModelState.IsValid)
                return Page();

            var result = await tweetRepo.AddTweets(Tweets, userIdClaim.Value);
            if (!result)
            {
                ModelState.AddModelError("UnableToTweet", "Unable to add tweet");
                return Page();
            }
            return Redirect("/SecuredPage/Tweet");

        }
    }
}