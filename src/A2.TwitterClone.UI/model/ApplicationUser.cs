using Maqduni.AspNetCore.Identity.RavenDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string MobileNumber { get; set; }
    }
}
