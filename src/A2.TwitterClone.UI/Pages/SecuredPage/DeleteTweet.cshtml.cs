using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.TwitterClone.UI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class DeleteTweetModel : PageModel
    {
        private readonly ITweetRepo tweetRepo;
        private ILogger<DeleteTweetModel> logger;
        public DeleteTweetModel(ITweetRepo repo, ILogger<DeleteTweetModel> logger)
        {
            tweetRepo = repo;
            this.logger = logger;
        }
        public async Task<IActionResult> OnGet(string tweetId)
        {
            if (string.IsNullOrWhiteSpace(tweetId))
            {
                ModelState.AddModelError("TweetIdEmpty", "Tweet Id cannot be empty");
            }
            var result =  await tweetRepo.DeleteTweet(tweetId);
            if (result == null)
                ModelState.AddModelError("TweetNotDeleted", "Tweet cannot be deleted");
            return LocalRedirect("/SecuredPage/Tweet");
        }
    }
}