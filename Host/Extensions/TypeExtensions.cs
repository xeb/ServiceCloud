using System;
using System.Linq;

namespace ServiceCloud.Extensions
{
	public static class TypeExtensions
	{
		/// <summary>
		/// Determines if the type implements the interfaceType
		/// </summary>
		/// <param name="type"></param>
		/// <param name="interfaceType"></param>
		/// <see cref="http://www.hanselman.com/blog/DoesATypeImplementAnInterface.aspx"/>
		/// <returns></returns>
		public static Boolean IsImplementationOf(this Type type, Type interfaceType)
		{
			return type.GetInterface(interfaceType.FullName) != null;
		}
	}
}