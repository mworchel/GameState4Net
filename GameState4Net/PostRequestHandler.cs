using Grapevine;
using Grapevine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    /// <summary>
    /// POST request handler which receives all the post requests which
    /// were send to the gamestate server
    /// </summary>
    public sealed class PostRequestHandler : RESTResource
    {
        [RESTRoute(Method = Grapevine.HttpMethod.POST)]
        public void HandleAllPostRequests(HttpListenerContext context)
        {
            var request = context.Request;

            // Get the server and the content
            var server = (Server as GameStateServer);
            var content = request.GetContent();

            // Make sure this handler is used by a gamestate server only
            if(server == null)
            {
                InternalServerError(context);
            }

            if (!string.IsNullOrEmpty(content))
            {   
                // At this point the content could still be no valid json/gamestate
                // so actually we should return BadRequest...well but we don't :x


                server.AddGameState(content);

                this.SendTextResponse(context, "Success");
            }
            else
            {
                // If no content was sent, it is a bad request
                BadRequest(context);
            }
        }

        private void BadRequest(HttpListenerContext context, string payload = "<h1>Bad Request</h1>", ContentType contentType = ContentType.HTML)
        {
            var buffer = Encoding.UTF8.GetBytes(payload);
            var length = buffer.Length;

            context.Response.StatusCode = 400;
            context.Response.StatusDescription = "Bad Request";
            context.Response.ContentType = ContentType.HTML.ToValue();

            FlushResponse(context, buffer, length);
        }
    }
}
