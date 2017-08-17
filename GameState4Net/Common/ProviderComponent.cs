using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.Common
{
	public class ProviderComponent : GameStateComponent
    {
		public ProviderComponent(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public ProviderComponent(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		[GameStateComponentValue("name")]
        public string Name { get; protected set; }

		[GameStateComponentValue("appid")]
		public string AppId { get; protected set; }

		[GameStateComponentValue("version")]
		public string Version { get; protected set; }

		[GameStateComponentValue("steamid")]
        public string SteamId { get; protected set; }

		[GameStateComponentValue("timestamp")]
        public string TimeStamp { get; protected set; }
    }
}
