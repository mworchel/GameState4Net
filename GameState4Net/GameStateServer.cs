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
        private List<Action<string>> callbacks = new List<Action<string>>();

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
            lock(callbacks)
            {
                foreach(var callback in callbacks)
                {
                    callback(json);
                }
            }
        }

        public void RegisterGameStateCallback(Action<string> callback)
        {
            lock (callbacks)
            {
                callbacks.Add(callback);
            }
        }
    }
}

