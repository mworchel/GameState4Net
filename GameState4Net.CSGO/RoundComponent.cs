using Newtonsoft.Json.Linq;

namespace GameState4Net.CSGO
{
	public enum Phase { Unknown, Live, Freezetime, Over};
    public enum BombStatus { Unknown, Planted, Exploded, Defused};
    public enum WinTeam { Unknown, T, CT};

    public class RoundComponent : GameStateComponent
	{
		public RoundComponent(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public RoundComponent(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}

		[GameStateComponentValue("phase")]
		public Phase Phase { get; protected set; }

		[GameStateComponentValue("bomb")]
		public BombStatus Bomb { get; protected set; }

		[GameStateComponentValue("win_team")]
		public WinTeam WinTeam { get; protected set; }
    }
}
