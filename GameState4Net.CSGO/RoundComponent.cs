using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net.CSGO
{
    public enum Phase { Unknown, Live, Freezetime, Over};
    public enum BombStatus { Unknown, Planted, Exploded, Defused};
    public enum WinTeam { Unknown, T, CT};

    public class RoundComponent : IGameStateComponent {
        private const string PhaseFieldIdentifier = "phase";

        private const string BombFieldIdentifier = "bomb";

        private const string WinTeamFieldIdentifier = "win_team";


        public void FromJson(string json) {
            var o = JObject.Parse(json);
            
            Phase = o.ValueEnum<Phase>(PhaseFieldIdentifier);
            Bomb = o.ValueEnum<BombStatus>(BombFieldIdentifier);
            WinTeam = o.ValueEnum<WinTeam>(WinTeamFieldIdentifier);
        }

        public string ComponentIdentifier
        {
            get { return "round"; }
        }


        public Phase Phase { get; private set; }

        public BombStatus Bomb { get; private set; }

        public WinTeam WinTeam { get; private set; }
    }
}
