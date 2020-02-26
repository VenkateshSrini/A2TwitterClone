using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using A2.TwitterClone.UI.Repository;
using Microsoft.AspNetCore.Http;

namespace A2.TwitterClone.UI
{
    public class EditTweetModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Tweets { get; set; }

       

        private readonly ITweetRepo tweetRepo;
        private ILogger<EditTweetModel> logger;
        public EditTweetModel(ITweetRepo tweetRepo,
            ILogger<EditTweetModel> logger)
        {
            this.tweetRepo = tweetRepo;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet(string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                ModelState.AddModelError("IDEMpty", "Tweet id is empty");
                return Page();
            }
            var tweet = await tweetRepo.GetTweetById(tweetId);
            if (tweet==null)
            {
                ModelState.AddModelError("TweetNot", "Tweet not exist");
                return Page();
            }
            else
            {
                Tweets = tweet.Squeeks;
                PageContext.HttpContext.Session.SetString("tweetId", tweet.Id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPost()
        {
            var tweetId = PageContext.HttpContext.Session.GetString("tweetId");
            PageContext.HttpContext.Session.Remove("tweetId");

            var tweet = await tweetRepo.GetTweetById(tweetId);
            if (tweet == null)
            {
                ModelState.AddModelError("TweetNot", "Tweet not exist");
                return Page();
            }
            else
            {
                tweet.Squeeks = Tweets;
                var result = await tweetRepo.EditTweets(tweet);
                if (result==null)
                    ModelState.AddModelError("TweetNotSaved", "Tweet not Saved");
                else
                {
                    return LocalRedirect("/SecuredPage/Tweet");
                }
                return Page();
            }

        }
    }
}