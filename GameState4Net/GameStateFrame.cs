using GameState4Net.Common;
using Newtonsoft.Json.Linq;

namespace GameState4Net
{
	/// <summary>
	/// 
	/// </summary>
	public class GameStateFrame : GameStateComponent
	{
		public GameStateFrame(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public GameStateFrame(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		[GameStateComponentValue("provider")]
		public ProviderComponent Provider { get; protected set; }
	}
}
