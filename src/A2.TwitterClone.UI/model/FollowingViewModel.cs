using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.model
{
    public class FollowingViewModel
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; } = "Follow";
    }
}
