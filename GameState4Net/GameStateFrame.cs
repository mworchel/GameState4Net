using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    /// <summary>
    /// 
    /// </summary>
    public class GameStateFrame : GameStateComponentContainer
    {
        private string json;

        public GameStateFrame()
            : base("")
        { }


        public override void FromJson(string json)
        {
            base.FromJson(json);

            this.json = json;
        }


        public string Json { get { return json; } }
    }
}
