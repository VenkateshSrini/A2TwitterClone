using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.TwitterClone.UI.model;
using A2.TwitterClone.UI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace A2.TwitterClone.UI
{
    public class TweetModel : PageModel
    {
        private readonly ITweetRepo tweetRepo;
        private ILogger<TweetModel> logger;
        public IList<Tweets> TweetsByUser;
        public const string nameIdentifer = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public TweetModel(ITweetRepo tweetRepo, ILogger<TweetModel> logger)
        {
            this.tweetRepo = tweetRepo;
            this.logger = logger;
        }
        public void OnGet()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim =>
            claim.Type.CompareTo(nameIdentifer)== 0);
            TweetsByUser = tweetRepo.GetAllTweetsForUser(userIdClaim.Value);
        }
    }
}