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

        [HttpPut]
        [RequireAuth]
        public ActionResult ToggleStar([FromBody] Comment comment)
        {
            User user = ((UserClaimsPrincipal)HttpContext.User).User;

            Comment existingComment = user.Stars.Find(c => c.CommentId == comment.CommentId);

            if (existingComment == null)
            {
                user.Stars.Add(comment);
            }
            else
            {
                user.Stars.Remove(existingComment);
            }

            userService.Update(user.Username, user);

            return Ok(user.Stars);
        }
    }
}
