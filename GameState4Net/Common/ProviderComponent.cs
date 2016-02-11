using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.Common
{
    public class ProviderComponent : IGameStateComponent
    {
        private const string NameFieldIdentifier = "name";

        private const string AppIdFieldIdentifier = "appid";

        private const string VersionFieldIdentifier = "version";

        private const string SteamIdFieldIdentifier = "steamid";

        private const string TimeStampFieldIdentifier = "timestamp";


        public void FromJson(string json)
        {
            var o = JObject.Parse(json);

            Name = o.Value<string>(NameFieldIdentifier);
            AppId = o.Value<string>(AppIdFieldIdentifier);
            Version = o.Value<string>(VersionFieldIdentifier);
            SteamId = o.Value<string>(SteamIdFieldIdentifier);
            TimeStamp = o.Value<string>(TimeStampFieldIdentifier);
        }

        public string ComponentIdentifier { get { return "provider"; } }


        public string Name { get; private set; }

        public string AppId { get; private set; }

        public string Version { get; private set; }

        public string SteamId { get; private set; }

        public string TimeStamp { get; private set; }
    }
}
