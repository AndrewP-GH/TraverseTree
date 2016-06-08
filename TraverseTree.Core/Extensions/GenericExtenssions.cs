using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	public static class GenericExtenssions
	{
		public static bool IsNull<T>(this T value) =>
			EqualityComparer<T>.Default.Equals(value, default(T));

		public static void NullGuard<T>(this T value, string argument)
		{
			if (value.IsNull()) {
				throw new ArgumentNullException(argument);
			}
		}

		public static void NullGuardAssign<T>(this T source, out T target, string argument)
		{
			source.NullGuard(argument);
			target = source;
		}
	}
}
