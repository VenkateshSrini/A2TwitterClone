using A2.TwitterClone.UI.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.Repository
{
    public interface ITweetRepo
    {
        Task<bool> AddTweets(string tweets, string userID);
        /*
         * User.Claims.FirstOrDefault(claim=>claim.Type=="http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
         */
        List<Tweets> GetAllTweetsForUser(string userID);
        Task<Tweets>GetTweetById(string tweetId);
        Task<Tweets> EditTweets(Tweets modfifiedTweet);
        Task<Tweets> DeleteTweet(string tweetId);
        


    }
}
