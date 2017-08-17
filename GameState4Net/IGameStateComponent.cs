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
		TValue GetValue<TValue>(string key);
    }
}
