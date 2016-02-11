using GameState4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.CSGO
{
    public enum PlayerActivity { Unknown, Menu, Playing, TextInput };

    public class PlayerComponent : GameStateComponentContainer
    {
        private const string SteamIdFieldIdentifier = "steamid";

        private const string NameFieldIdentifier = "name";

        private const string TeamFieldIdentifier = "team";

        private const string ActivityFieldIdentifier = "activity";


        public PlayerComponent()
            : base("player")
        { }


        public override void FromJson(string json)
        {
            base.FromJson(json);

            SteamId = ComponentJObject.Value<string>(SteamIdFieldIdentifier);
            Name = ComponentJObject.Value<string>(NameFieldIdentifier);
            Team = ComponentJObject.Value<string>(TeamFieldIdentifier);
            Activity = ComponentJObject.ValueEnum<PlayerActivity>(ActivityFieldIdentifier);
        }


        public string SteamId { get; private set; }

        public string Name { get; private set; }

        public string Team { get; private set; }

        public PlayerActivity Activity { get; private set; }
    }
}
