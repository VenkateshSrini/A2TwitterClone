using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.model
{
    public class Following
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; }
        public List<string> Followings { get; set; } = new List<string>();
    }
}
