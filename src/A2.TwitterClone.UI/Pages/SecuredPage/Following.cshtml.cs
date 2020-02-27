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
    public class FollowingModel : PageModel
    {
        private readonly IFollowingRepo followingRepo;
        private readonly ILogger<FollowingModel> logger;
        public const string nameIdentifer = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        [BindProperty]
        public List<FollowingViewModel> FollowingViewModels { get; set; } = new List<FollowingViewModel>();
        public FollowingModel(IFollowingRepo followingRepo, 
            ILogger<FollowingModel> logger)
        {
            this.followingRepo = followingRepo;
            this.logger = logger;
        }
        public  async Task<IActionResult> OnGet()
        {
            await LoadPageContent();
            return Page();
        }
        public async Task<IActionResult> OnPostManageFollow(string userId, string tAction)
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim =>
            claim.Type.CompareTo(nameIdentifer) == 0);

            if (tAction.ToLower()=="follow")
            {
                var result = await followingRepo.AddFollower(userIdClaim.Value, userId);
                if (!result)
                    ModelState.AddModelError("UnableToAddFollowing", "Follwer not added");
            }
            else if (tAction.ToLower()=="unfollow")
            {
                var result = await followingRepo.DeleteFollower(userIdClaim.Value, userId);
                if (!result)
                    ModelState.AddModelError("UnableToDeleteFollowing", "Follwer not Deleted");
            }
            await LoadPageContent();
            return Page();
        }
        private async Task LoadPageContent()
        {
            var userIdClaim = User.Claims.FirstOrDefault(claim =>
            claim.Type.CompareTo(nameIdentifer) == 0);
            //var getAllUserTask = followingRepo.GetAllUsers();
            //var getFollowinUserTask = followingRepo.GetFollowingForUser(userIdClaim.Value);
            //Task.WaitAll(getAllUserTask, getFollowinUserTask);
            var allUsers = await followingRepo.GetAllUsers(userIdClaim.Value);
            var following = await followingRepo.GetFollowingForUser(userIdClaim.Value);

            foreach (var user in allUsers)
            {
                FollowingViewModels.Add(new FollowingViewModel
                {
                    UserName = user.UserName,
                    UserId = user.Id,
                    Action = ((following != null) && (following.Followings.Contains(user.Id))) ?
                            "UnFollow" : "Follow"
                });

            }
        }
    }
}