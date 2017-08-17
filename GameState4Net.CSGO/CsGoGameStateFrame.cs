using Newtonsoft.Json.Linq;

namespace GameState4Net.CSGO
{
	public class CsGoGameStateFrame : GameStateFrame
	{
		public CsGoGameStateFrame(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public CsGoGameStateFrame(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		[GameStateComponentValue("player")]
		public PlayerComponent Player { get; protected set; }

		[GameStateComponentValue("round")]
		public RoundComponent Round { get; protected set; }
	}
}
