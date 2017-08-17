using Newtonsoft.Json.Linq;

namespace GameState4Net.CSGO
{
	public enum WeaponState { Unknown, Holstered, Active, Reloading };

	public class WeaponComponent : GameStateComponent
	{
		public WeaponComponent(string json)
			: base(json)
		{
			DeserializeProperties();
		}

		public WeaponComponent(JObject jsonObject)
			: base(jsonObject)
		{
			DeserializeProperties();
		}


		//name ;weapon name
		[GameStateComponentValue("name")]
		public string Name { get; protected set; }

		//paintkit ;appears to be an internally used skin name, or "default" if none
		[GameStateComponentValue("paintkit")]
		public string Paintkit { get; protected set; }

		//type ;"Knife", "Pistol", "Submachine Gun", etc.
		[GameStateComponentValue("type")]
		public string Type { get; protected set; }

		//state ;"holstered" or "active"
		[GameStateComponentValue("state")]
		public WeaponState State { get; protected set; }

		//ammo_clip ;int, amount of current ammo in clip
		[GameStateComponentValue("ammo_clip")]
		public int? AmmoClip { get; protected set; }

		//ammo_clip_max ;int, max amount of ammo a clip can hold
		[GameStateComponentValue("ammo_clip_max")]
		public int? AmmoClipMax { get; protected set; }

		//ammo_reserve ;int, amount of reserve ammo, not in clip
		[GameStateComponentValue("ammo_reserve")]
		public int? AmmoReserve { get; protected set; }
	}
}
