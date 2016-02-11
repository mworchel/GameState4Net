using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.CSGO
{
    public enum WeaponState { Unknown, Holstered, Active, Reloading };

    public class Weapon
    {
        private const string NameFieldId = "name";

        private const string PaintkitFieldId = "paintkit";

        private const string TypeFieldId = "type";

        private const string StateFieldId = "state";

        private const string AmmoClipFieldId = "ammo_clip";

        private const string AmmoClipMaxFieldId = "ammo_clip_max";

        private const string AmmoReserveFieldId = "ammo_reserve";


        public Weapon(JObject jsonObject)
        {
            Name = jsonObject.Value<string>(NameFieldId);
            Paintkit = jsonObject.Value<string>(PaintkitFieldId);
            Type = jsonObject.Value<string>(TypeFieldId);

            State = jsonObject.ValueEnum<WeaponState>(StateFieldId);

            AmmoClip = jsonObject.Value<int?>(AmmoClipFieldId);
            AmmoClipMax = jsonObject.Value<int?>(AmmoClipMaxFieldId);
            AmmoReserve = jsonObject.Value<int?>(AmmoReserveFieldId);
        }


        //name ;weapon name
        public string Name { get; private set; }

        //paintkit ;appears to be an internally used skin name, or "default" if none
        public string Paintkit { get; private set; }

        //type ;"Knife", "Pistol", "Submachine Gun", etc.
        public string Type { get; private set; }

        //state ;"holstered" or "active"
        public WeaponState State { get; private set; }

        //ammo_clip ;int, amount of current ammo in clip
        public int? AmmoClip { get; private set; }

        //ammo_clip_max ;int, max amount of ammo a clip can hold
        public int? AmmoClipMax { get; private set; }

        //ammo_reserve ;int, amount of reserve ammo, not in clip
        public int? AmmoReserve { get; private set; }
    }

    public class PlayerWeaponsComponent : IGameStateComponent
    {
        public PlayerWeaponsComponent()
        {
            Weapons = new List<Weapon>();
        }

        public void FromJson(string json)
        {
            JObject o = JObject.Parse(json);

            foreach (var pair in o)
            {
                if (pair.Value is JObject)
                {
                    Weapons.Add(new Weapon(pair.Value as JObject));
                }
            }
        }

        public string ComponentIdentifier { get { return "weapons"; } }


        public List<Weapon> Weapons { get; private set; }
    }
}
