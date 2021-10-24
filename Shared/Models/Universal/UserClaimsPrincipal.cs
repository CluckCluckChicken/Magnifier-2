using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared.Models.Universal
{
    public class UserClaimsPrincipal : ClaimsPrincipal
    {
        public User User { get; set; }

        public UserClaimsPrincipal(User user)
        {
            User = user;
        }
    }
}
