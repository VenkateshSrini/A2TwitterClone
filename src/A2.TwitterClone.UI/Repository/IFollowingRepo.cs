using A2.TwitterClone.UI.model;
using A2.TwitterClone.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.Repository
{
    public interface IFollowingRepo
    {
        public Task<bool> AddFollower(string userId, string addFollowingUserId);
        public Task<bool> DeleteAllFollower(string Userid);
        public Task<bool> DeleteFollower(string userid, string followinUserId);
        public Task<Following> GetFollowingForUser(string userId);
        public Task<List<ApplicationUser>> GetAllUsers(string userId);

    }
}
