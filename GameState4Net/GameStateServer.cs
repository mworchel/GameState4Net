using Grapevine.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    public class GameStateServer : RESTServer
    {
        private List<Action<GameStateFrame>> callbacks = new List<Action<GameStateFrame>>();


        public GameStateServer(Config config)
            : base(config)
        {
        }

        public GameStateServer(string host = "localhost", string port = "1234", string protocol = "http", string dirindex = "index.html", string webroot = null, int maxthreads = 5)
            : base(host, port, protocol, dirindex, webroot, maxthreads)
        {
        }


        public void AddGameState(string json)
        {
            if(!string.IsNullOrEmpty(json))
            {
                // Create a frame object and parse the json
                GameStateFrame frame = new GameStateFrame();
                frame.FromJson(json);

                // Let the callbacks handle the frame
                lock (callbacks)
                {
                    foreach (var callback in callbacks)
                    {
                        callback(frame);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Callbacks will most certainly be called from a thread different than the main thread</remarks>
        /// <param name="callback"></param>
        public void RegisterGameStateCallback(Action<GameStateFrame> callback)
        {
            lock (callbacks)
            {
                callbacks.Add(callback);
            }
        }
    }
}

