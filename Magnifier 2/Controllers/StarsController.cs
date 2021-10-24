using Magnifier_2.Attributes;
using Magnifier_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Universal;

namespace Magnifier_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarsController : ControllerBase
    {
        private UserService userService;

        public StarsController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [RequireAuth]
        public ActionResult GetStars()
        {
            User user = ((UserClaimsPrincipal)HttpContext.User).User;

            return Ok(user.Stars);
        }

        [HttpPut("{commentId}")]
        [RequireAuth]
        public ActionResult ToggleStar(int commentId)
        {
            User user = ((UserClaimsPrincipal)HttpContext.User).User;

            if (user.Stars.Contains(commentId))
            {
                user.Stars.Remove(commentId);
            }
            else
            {
                user.Stars.Add(commentId);
            }


            userService.Update(user.Username, user);

            return Ok(user.Stars);
        }
    }
}
