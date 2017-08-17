using Newtonsoft.Json.Linq;

namespace GameState4Net.CSGO
{
	public class PlayerStateComponent : GameStateComponent
    {
		public PlayerStateComponent(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public PlayerStateComponent(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		//health ;int, 0-100, current HP
		[GameStateComponentValue("health")]
        public int? Health { get; protected set; }

		//armor ;int, 0-100, current armor
		[GameStateComponentValue("armor")]
		public int? Armor { get; protected set; }

		//helmet ;bool, true/false if they have helm armor
		[GameStateComponentValue("helmet")]
		public bool? Helmet { get; protected set; }

		//flashed ;int, 0-255 depending on how flashed the player is
		[GameStateComponentValue("flashed")]
		public int? Flashed { get; protected set; }

		//smoked ;same as above, but for smoke
		[GameStateComponentValue("smoked")]
		public int? Smoked { get; protected set; }

		//burning ;same as above, but for fire
		[GameStateComponentValue("burning")]
		public int? Burning { get; protected set; }

		//money ;current money
		[GameStateComponentValue("money")]
		public int? Money { get; protected set; }

		//round_kills ;how many kills the player got in that round
		[GameStateComponentValue("round_kills")]
		public int? RoundKills { get; protected set; }

		//round_killhs ;how many headshot kills the player got (hs kills increase this and the above's number)
		[GameStateComponentValue("round_killhs")]
		public int? RoundKillsHS { get; protected set; }
    }
}
