using Microsoft.AspNetCore.Mvc;
using Shared.Models.Universal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Magnifier_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScratchController : ControllerBase
    {
        private readonly HttpClient client;

        public ScratchController()
        {
            client = new HttpClient();
        }

        [HttpGet("projects/{id}")]
        public async Task<ActionResult> GetProject([FromRoute] string id)
        {
            string response;

            try
            {
                response = await client.GetStringAsync($"https://api.scratch.mit.edu/projects/{id}");
            }
            catch
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Deserialize<Project>(response));
        }

        [HttpGet("users/{username}")]
        public async Task<ActionResult> GetUser([FromRoute] string username)
        {
            string response;

            try
            {
                response = await client.GetStringAsync($"https://api.scratch.mit.edu/users/{username}");
            }
            catch
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Deserialize<ScratchUser>(response));
        }

        [HttpGet("studios/{studioId}")]
        public async Task<ActionResult> GetStudio([FromRoute] string studioId)
        {
            string response;

            try
            {
                response = await client.GetStringAsync($"https://api.scratch.mit.edu/studios/{studioId}");
            }
            catch
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Deserialize<Studio>(response));
        }
    }
}
