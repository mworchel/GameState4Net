using GameState4Net.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameState4Net
{
	/// <summary>
	/// </summary>
	public class GameStateComponent : IGameStateComponent
	{
		private readonly JObject jsonObject;


		public GameStateComponent(string json)
			: this(JObject.Parse(json))
		{
		}

		public GameStateComponent(JObject jsonObject)
		{
			this.jsonObject = jsonObject;
		}


		public TValue GetValue<TValue>(string key)
		{
			return (TValue)GetValue(typeof(TValue), key);
		}

		public object GetValue(Type type, string key)
		{
			var token = jsonObject[key];

			return token != null ? GetValue(type, token) : null;
		}

		public object GetValue(Type type, JToken token)
		{
			object result = null;

			if (typeof(IGameStateComponent).IsAssignableFrom(type))
			{
				if (token is JObject)
				{
					result = Activator.CreateInstance(type, new[] { (JObject)token });
				}
			}
			else
			{
				result = ParseSimpleType(type, token.ToString());
			}

			return result;
		}


		protected void DeserializeProperties()
		{
			var properties = GetWriteableAttributedProperties<GameStateComponentValueAttribute>();

			var listProperties = properties.Where(p => typeof(IEnumerable).IsAssignableFrom(p.PropertyType) && !typeof(string).IsAssignableFrom(p.PropertyType));
			var valueProperties = properties.Except(listProperties);
			DeserializeListProperties(listProperties);
			DeserializeValueProperties(valueProperties);
		}


		private IEnumerable<PropertyInfo> GetWriteableAttributedProperties<TAttribute>() where TAttribute : Attribute
		{
			var type = this.GetType();
			return type.GetProperties().Where((p) => p.HasCustomAttribute<TAttribute>(true) && p.CanWrite);
		}
		
		private void DeserializeValueProperties(IEnumerable<PropertyInfo> properties)
		{
			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttribute<GameStateComponentValueAttribute>();
				var propertyType = property.PropertyType;

				var value = GetValue(propertyType, attribute.Identifier);

				if (value != null)
				{
					property.SetValue(this, value);
				}
			}
		}

		private void DeserializeListProperties(IEnumerable<PropertyInfo> properties)
		{
			foreach (var property in properties)
			{
				var propertyType = property.PropertyType;

				// Determine the type of items in the list represented by the property
				Type itemType = propertyType
					.GetInterfaces()
					.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
					.GetGenericArguments()[0];

				if(itemType == null)
				{
					continue;
				}

				// Try to get the list from the jobject
				var attribute = property.GetCustomAttribute<GameStateComponentValueAttribute>();
				var token = jsonObject.GetValue(attribute.Identifier);

				// Validate the token
				if(token == null || !(token is JArray))
				{
					continue;
				}

				JArray array = (token as JArray);
				ArrayList list = new ArrayList(array.Count);
				foreach(var arrayToken in array)
				{
					var item = GetValue(itemType, arrayToken);

					if(item != null)
					{
						array.Add(item);
					}
				}

				property.SetValue(this, list);
			}
		}

		private object ParseSimpleType(Type type, string value)
		{
			object result = null;
			if (type.IsEnum)
			{
				try
				{
					result = Enum.Parse(type, value);
				}
				catch { }
			}
			else
			{
				try
				{
					result = Convert.ChangeType(value, type);
				}
				catch { }
			}

			return result;
		}
	}
}
