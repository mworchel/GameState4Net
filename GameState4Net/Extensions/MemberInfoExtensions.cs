using System;
using System.Reflection;

namespace GameState4Net.Extensions
{
	public static class MemberInfoExtensions
	{
		public static bool HasCustomAttribute<TAttribute>(this MemberInfo self, bool inherit) where TAttribute : Attribute
		{
			return self.GetCustomAttribute<TAttribute>(inherit) != null;
		}
	}
}
