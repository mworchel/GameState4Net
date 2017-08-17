using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GameState4Net.CSGO
{
	public enum PlayerActivity { Unknown, Menu, Playing, TextInput };

	public class PlayerComponent : GameStateComponent
	{
		public PlayerComponent(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public PlayerComponent(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		[GameStateComponentValue("steamid")]
		public string SteamId { get; protected set; }

		[GameStateComponentValue("name")]
		public string Name { get; protected set; }

		[GameStateComponentValue("team")]
		public string Team { get; protected set; }

		[GameStateComponentValue("activity")]
		public PlayerActivity Activity { get; protected set; }

		[GameStateComponentValue("state")]
		public PlayerStateComponent State { get; protected set; }


		[GameStateComponentValue("weapons")]
		public IEnumerable<WeaponComponent> Weapons { get; protected set; }
    }
}
