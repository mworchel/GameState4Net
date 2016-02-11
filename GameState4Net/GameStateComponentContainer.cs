using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    /// <summary>
    /// A <see cref="GameStateComponentContainer"/> is a <see cref="GameStateComponent"/> itself
    /// but other <see cref="GameStateComponent"/> instances can be obtained from it, too.
    /// </summary>
    public class GameStateComponentContainer : IGameStateComponent
    {
        private JObject componentJObject;

        private string componentIdentifier;


        public GameStateComponentContainer(string componentIdentifier) {
            this.componentIdentifier = componentIdentifier;
        }


        public T GetComponent<T>() where T : class, IGameStateComponent {
            T component = Activator.CreateInstance<T>();
            return GetComponent(component);
        }

        public T GetComponent<T>(Func<T> factory) where T : class, IGameStateComponent
        {
            T component = factory();
            return GetComponent(component);
        }

        private T GetComponent<T>(T component) where T : class, IGameStateComponent
        {
            var obj = componentJObject[component.ComponentIdentifier];

            if (obj == null || !(obj is JObject))
            {
                //throw new InvalidOperationException("No component of class " + typeof(T).Name);
                return default(T);
            }

            var rootFieldContent = obj.ToString();

            component.FromJson(rootFieldContent);

            return component;
        }


        public virtual void FromJson(string json)
        {
            componentJObject = JObject.Parse(json);
        }

        public string ComponentIdentifier
        {
            get { return componentIdentifier; }
        }


        protected JObject ComponentJObject { get { return componentJObject; } }
    }
}
