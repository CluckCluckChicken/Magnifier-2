using Magnifier_2.Attributes;
using Magnifier_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Universal;

namespace Magnifier_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [RequireAuth]
        public ActionResult GetStars()
        {
            User user = ((UserClaimsPrincipal)HttpContext.User).User;

            return Ok(user);
        }
    }
}
