using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.TwitterClone.UI.model;
using Microsoft.Extensions.Logging;
using Raven.Client;
using Raven.Client.Documents.Session;

namespace A2.TwitterClone.UI.Repository
{
    public class TweetRepo:ITweetRepo
    {
        private IAsyncDocumentSession asyncDocumentSession;
        private ILogger<TweetRepo> logger;
        public TweetRepo(IAsyncDocumentSession asyncDocumentSession, 
            ILogger<TweetRepo> logger)
        {
            this.asyncDocumentSession = asyncDocumentSession;
            this.logger = logger;
        }

        public async Task<bool> AddTweets(string tweets, string userID)
        {
            var tweetModel = new Tweets();
            tweetModel.Squeeks = tweets;
            tweetModel.UserID = userID;
            try
            {
                await asyncDocumentSession.StoreAsync(tweetModel);
                await asyncDocumentSession.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }

        public async Task<Tweets> DeleteTweet(string tweetId)
        {
            //asyncDocumentSession.Advanced.DocumentStore.Conventions.
            var tweets = await asyncDocumentSession.LoadAsync<Tweets>(tweetId);
            asyncDocumentSession.Delete<Tweets>(tweets);
            await asyncDocumentSession.SaveChangesAsync();
            return tweets;
        }

        public async Task<Tweets> EditTweets(Tweets modfifiedTweet)
        {
            var tweets = await asyncDocumentSession.LoadAsync<Tweets>(modfifiedTweet.Id);
            tweets.Squeeks = modfifiedTweet.Squeeks;
            await asyncDocumentSession.SaveChangesAsync();
            return tweets;

        }

        public List<Tweets> GetAllTweetsForUser(string userID)
        {
            return asyncDocumentSession.Query<Tweets>()
                .Where(tweet => tweet.UserID.CompareTo(userID) == 0)
                .ToList();
        }

        public async Task<Tweets> GetTweetById(string tweetId)
        {
            return await asyncDocumentSession.LoadAsync<Tweets>(tweetId);
        }
    }
}
