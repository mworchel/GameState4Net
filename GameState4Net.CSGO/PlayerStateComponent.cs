using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.CSGO
{
    public class PlayerStateComponent : IGameStateComponent
    {
        private const string HealthFieldId = "health";

        private const string ArmorFieldId = "armor";

        private const string HelmetFieldId = "helmet";

        private const string FlashedFieldId = "flashed";

        private const string SmokedFieldId = "smoked";

        private const string BurningFieldId = "burning";

        private const string MoneyFieldId = "money";

        private const string RoundKillsFieldId = "round_kills";

        private const string RoundKillsHSFieldId = "round_killhs";


        public void FromJson(string json)
        {
            JObject o = JObject.Parse(json);

            Health = o.Value<int?>(HealthFieldId);
            Armor = o.Value<int?>(ArmorFieldId);
            Helmet = o.Value<bool?>(HelmetFieldId);

            Flashed = o.Value<int?>(FlashedFieldId);
            Smoked = o.Value<int?>(SmokedFieldId);
            Burning = o.Value<int?>(BurningFieldId);

            Money = o.Value<int?>(MoneyFieldId);
            RoundKills = o.Value<int?>(RoundKillsFieldId);
            RoundKillsHS = o.Value<int?>(RoundKillsHSFieldId);
        }

        public string ComponentIdentifier { get { return "state"; } }


        //health ;int, 0-100, current HP
        public int? Health { get; private set; }

        //armor ;int, 0-100, current armor
        public int? Armor { get; private set; }

        //helmet ;bool, true/false if they have helm armor
        public bool? Helmet { get; private set; }

        //flashed ;int, 0-255 depending on how flashed the player is
        public int? Flashed { get; private set; }

        //smoked ;same as above, but for smoke
        public int? Smoked { get; private set; }

        //burning ;same as above, but for fire
        public int? Burning { get; private set; }

        //money ;current money
        public int? Money { get; private set; }

        //round_kills ;how many kills the player got in that round
        public int? RoundKills { get; private set; }

        //round_killhs ;how many headshot kills the player got (hs kills increase this and the above's number)
        public int? RoundKillsHS { get; private set; }
    }
}
