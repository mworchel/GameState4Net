using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
  [System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
  public sealed class GameStateComponentValueAttribute : Attribute
  {
    readonly string identifier;

    public GameStateComponentValueAttribute(string identifier)
    {
      this.identifier = identifier;
    }

    public string Identifier
    {
      get { return identifier; }
    }
  }
}
