using HtmlAgilityPack;
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
    public class CommentsController : ControllerBase
    {
        private readonly HttpClient client;

        public CommentsController()
        {
            client = new HttpClient();
        }

        [HttpGet]
        [Route("sapi/{resource}/{id}/{page}")]
        [Route("sapi/{owner}/{resource}/{id}/{page}")]
        public async Task<ActionResult> GetSAPIComments(string owner /* potatophant, etc */, string resource /* projects, studios, etc */, string id /* 30136012, etc*/, int page)
        {
            string response;

            string url = $"https://api.scratch.mit.edu/users/{owner}/{resource}/{id}/comments";

            if (resource == "studios")
            {
                url = $"https://api.scratch.mit.edu/studios/{id}/comments";
            }

            try
            {
                response = await client.GetStringAsync($"{url}?offset={(page - 1) * 20}");
            }
            catch
            {
                return NotFound();
            }

            List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(response);

            foreach (Comment comment in comments)
            {
                comment.Content = Constants.MENTION_REGEX.Replace(comment.Content, Constants.MENTION_REGEX_EVALUATOR);

                comment.Replies = JsonSerializer.Deserialize<List<Comment>>(await client.GetStringAsync($"{url}/{comment.CommentId}/replies"));

                foreach (Comment reply in comment.Replies)
                {
                    reply.Content = Constants.MENTION_REGEX.Replace(reply.Content, Constants.MENTION_REGEX_EVALUATOR);
                }
            }

            return Ok(comments);
        }

        [HttpGet("legacy/{resource}/{id}/{page}")]
        public async Task<ActionResult> GetLegacyComments(string resource /* user, etc */, string id /* potatophant, etc */, int page) // get comments from scratchr2 pages
        {
            string response;

            try
            {
                response = await client.GetStringAsync($"https://scratch.mit.edu/site-api/comments/{resource}/{id}?page={page}");
            }
            catch
            {
                return NotFound();
            }

            HtmlDocument html = new HtmlDocument();

            html.LoadHtml(response);

            HtmlNodeCollection commentNodes = html.DocumentNode.SelectNodes("//div[@class=\"comment \"]");

            List<Comment> comments = new List<Comment>();

            if (commentNodes == null)
            {
                return Ok(JsonSerializer.Serialize(comments));
            }

            foreach (HtmlNode node in commentNodes)
            {
                HtmlNode info = node.SelectSingleNode(".//div[@class=\"info\"]");
                HtmlNode user = node.SelectSingleNode(".//a[@id=\"comment-user\"]");
                Author author = new Author(info.SelectSingleNode(".//div[@class=\"name\"]").InnerText.Trim(), user.SelectSingleNode(".//img[@class=\"avatar\"]").Attributes["src"].Value);
                Comment comment = new Comment(int.Parse(node.Attributes["data-comment-id"].Value), Constants.MENTION_REGEX.Replace(info.SelectSingleNode(".//div[@class=\"content\"]").InnerText.Trim().Replace("\n      ", ""), Constants.MENTION_REGEX_EVALUATOR), DateTime.Parse(info.SelectSingleNode(".//span[@class=\"time\"]").Attributes["title"].Value), author);

                if (!node.ParentNode.HasClass("reply"))
                {
                    foreach (HtmlNode replyContainer in node.ParentNode.SelectSingleNode(".//ul[@class=\"replies\"]").ChildNodes)
                    {
                        if (replyContainer.SelectSingleNode(".//div[@class=\"comment \"]") != null)
                        {
                            HtmlNode replyContainerInfo = replyContainer.SelectSingleNode(".//div[@class=\"info\"]");
                            HtmlNode replyContainerUser = replyContainer.SelectSingleNode(".//a[@id=\"comment-user\"]");
                            Author replyAuthor = new Author(replyContainerInfo.SelectSingleNode(".//div[@class=\"name\"]").InnerText.Trim(), replyContainerUser.SelectSingleNode(".//img[@class=\"avatar\"]").Attributes["src"].Value);
                            Comment reply = new Comment(int.Parse(replyContainer.SelectSingleNode(".//div[@class=\"comment \"]").Attributes["data-comment-id"].Value), Constants.MENTION_REGEX.Replace(replyContainerInfo.SelectSingleNode(".//div[@class=\"content\"]").InnerText.Trim().Replace("\n      ", ""), Constants.MENTION_REGEX_EVALUATOR), DateTime.Parse(replyContainerInfo.SelectSingleNode(".//span[@class=\"time\"]").Attributes["title"].Value), replyAuthor);

                            comment.Replies.Add(reply);
                        }
                    }

                    comments.Add(comment);
                }
            }

            return Ok(comments);
        }
    }
}
