using Maqduni.AspNetCore.Identity.RavenDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.TwitterClone.UI.model
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
