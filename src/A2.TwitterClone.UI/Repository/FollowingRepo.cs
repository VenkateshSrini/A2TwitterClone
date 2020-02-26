using A2.TwitterClone.UI.model;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.Repository
{
    public class FollowingRepo:IFollowingRepo
    {
        private IAsyncDocumentSession asyncDocumentSession;
        private ILogger<FollowingRepo> logger;

        public FollowingRepo (IAsyncDocumentSession asyncDocumentSession,
            ILogger<FollowingRepo> logger)
        {
            this.asyncDocumentSession = asyncDocumentSession;
            this.logger = logger;
        }

        public async Task<bool> AddFollower(string userId, string addFollowingUserId)
        {
            var following = await asyncDocumentSession.Advanced.AsyncDocumentQuery<Following>()
                                                       .WhereEquals(following=>following.UserId, userId)
                                                       .SingleOrDefaultAsync()
                                                       ?? new Following{
                                                                UserId = userId
                                                            };
            following.Followings.Add(addFollowingUserId);
            if (string.IsNullOrWhiteSpace(following.Id))
                await asyncDocumentSession.StoreAsync(following);
            await asyncDocumentSession.SaveChangesAsync();
            return true;


        }

        public async Task<bool> DeleteAllFollower(string userId)
        {
           var followingDoc= await asyncDocumentSession.Advanced.AsyncDocumentQuery<Following>()
                                               .WhereEquals(following =>
                                               following.UserId, userId)
                                               .SingleOrDefaultAsync();
            asyncDocumentSession.Delete<Following>(followingDoc);
            await asyncDocumentSession.SaveChangesAsync();
            return true;
                                                   
        }

        public async Task<bool> DeleteFollower(string userId, string followinUserId)
        {
            var followingDoc = await asyncDocumentSession.Advanced.AsyncDocumentQuery<Following>()
                                              .WhereEquals(following =>
                                              following.UserId, userId)
                                              .SingleOrDefaultAsync();
            if ((followingDoc.Followings.Count == 1) &&
                    (string.Compare(followingDoc.Followings[0], followinUserId) == 0))
                asyncDocumentSession.Delete<Following>(followingDoc);
            else if (followingDoc.Followings.Count > 0)
                followingDoc.Followings.Remove(followinUserId);
            await asyncDocumentSession.SaveChangesAsync();
            return true;

        }

        public async Task<Following> GetFollowingForUser(string userId)
        {


            return await asyncDocumentSession.Advanced.AsyncDocumentQuery<Following>()
                                               .WhereEquals(following =>
                                               following.UserId, userId)
                                               .SingleOrDefaultAsync();
        }
    }
}
