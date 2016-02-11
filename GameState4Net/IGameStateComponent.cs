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
    public interface IGameStateComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        void FromJson(string json);

        /// <summary>
        /// 
        /// </summary>
        string ComponentIdentifier { get; }
    }
}
