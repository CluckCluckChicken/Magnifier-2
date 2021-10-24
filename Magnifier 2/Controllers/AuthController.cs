using Magnifier_2.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.MongoDB;
using Shared.Models.Universal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Magnifier_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthCodeService authCodeService;
        private UserService userService;
        private SessionService sessionService;

        public AuthController(AuthCodeService authCodeService, UserService userService, SessionService sessionService)
        {
            this.authCodeService = authCodeService;
            this.userService = userService;
            this.sessionService = sessionService;
        }

        [HttpGet("code")]
        public ActionResult GetCode()
        {
            int len = 36;

            Random rnd = new Random();
            StringBuilder b = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                b.Append(Constants.CONSONANTS[rnd.Next(Constants.CONSONANTS.Length)]);
            }
            string result = b.ToString();

            authCodeService.Create(new AuthCode(result));
            return Ok(result);
        }

        [HttpGet("token")]
        public async Task<ActionResult> GetTokenAsync(string code)
        {
            foreach (AuthCode authCode in authCodeService.Get())
            {
                if (authCode.code == code && authCode.hasBeenUsed == false)
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(Constants.authProject);
                    var data = await response.Content.ReadAsStringAsync();

                    List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(data);

                    foreach (Comment comment in comments)
                    {
                        if (comment.Content == code)
                        {
                            authCodeService.Update(code, new AuthCode(code, true));

                            Shared.Models.Universal.User user = userService.Get(comment.Author.Username);

                            if (user == null)
                            {
                                user = userService.Create(new(comment.Author));
                            }
                            else if (user.IsBanned)
                            {
                                return Forbid();
                            }

                            user.LastLogin = DateTime.Now;

                            userService.Update(user.Username, user);

                            return Ok(sessionService.Create(new Session(user.Username)).SessionId);
                        }
                    }
                }
            }

            return Unauthorized();
        }
    }
}
