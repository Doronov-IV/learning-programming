using System.Reflection;

namespace ServerCore
{
	internal static class TypeExtentions
	{
		public static bool ContainsAttribute(this Type type, Type attrType)
		{
			return type.GetCustomAttribute(attrType) != null;
		}
	}
}
