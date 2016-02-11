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
        public void HandleAllPostsRequests(HttpListenerContext context)
        {
            var request = context.Request;

            // Get the server and the content
            var server = (Server as GameStateServer);
            var content = request.GetContent();

            if (!string.IsNullOrEmpty(content))
            {
                server.AddGameState(content);
            }

            this.SendTextResponse(context, "Success");
        }
    }
}
