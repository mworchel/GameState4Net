using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
    public static class JTokenExtensions
    {
        public static T ValueEnum<T>(this JToken self, object key = null)
        {
            var stringValue = self.Value<string>(key);
            object intermediateResult = null;
            try
            {
                intermediateResult = (T)Enum.Parse(typeof(T), stringValue, true);
            }
            catch (Exception)
            { }

            var result = intermediateResult != null ? (T)intermediateResult : default(T);

            return result;
        }
    }
}
