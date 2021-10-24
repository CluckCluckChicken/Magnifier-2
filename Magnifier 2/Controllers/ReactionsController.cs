using Magnifier_2.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Universal;

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
        public ActionResult PutReaction([FromRoute] int commentId, [FromRoute] string emoji)
        {
            Reaction existingReaction = reactionService.Get(commentId, "potatophant", emoji);

            if (existingReaction == null)
            {
                reactionService.Create(new Reaction(commentId, "potatophant", emoji));
            }
            else
            {
                reactionService.Remove(existingReaction);
            }

            return Ok(reactionService.Get(commentId));
        }
    }
}
