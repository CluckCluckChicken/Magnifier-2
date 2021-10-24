using Magnifier_2.Attributes;
using Magnifier_2.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Universal;
using System.Linq;

namespace Magnifier_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private ReactionService reactionService;

        public ReactionsController(ReactionService reactionService)
        {
            this.reactionService = reactionService;
        }

        [HttpGet("{commentId}")]
        public ActionResult GetReactions([FromRoute] int commentId)
        {
            return Ok(reactionService.Get(commentId));
        }

        [HttpPut("{commentId}/{emoji}")]
        [RequireAuth]
        public ActionResult PutReaction([FromRoute] int commentId, [FromRoute] string emoji)
        {
            User user = ((UserClaimsPrincipal)HttpContext.User).User;

            if (!(Constants.EMOJIS.Contains(emoji) || user.IsAdmin || reactionService.Get(commentId, emoji) != null))
            {
                return Forbid("You don't have sufficient permissions to react with that emoji");
            }

            Reaction existingReaction = reactionService.Get(commentId, user.Username, emoji);

            if (existingReaction == null)
            {
                reactionService.Create(new Reaction(commentId, user.Username, emoji));
            }
            else
            {
                reactionService.Remove(existingReaction);
            }

            return Ok(reactionService.Get(commentId));
        }
    }
}
