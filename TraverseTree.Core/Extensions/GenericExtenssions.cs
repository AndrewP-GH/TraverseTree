using System;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	/// <summary>
	/// Extension class for generic
	/// </summary>
	internal static class GenericExtenssions
	{
		/// <summary>
		/// Perform compare with null without boxing-unboxing
		/// </summary>
		/// <typeparam name="T">Generic type</typeparam>
		/// <param name="value">Value</param>
		/// <returns>
		/// If T is reference type, then perform compare with null. 
		/// If T is value type, then perform compate with default
		/// If T is <see cref="Nullable{T}"/> then pefrom compare with null for nullable types
		/// </returns>
		public static bool IsNull<T>(this T value) =>
			EqualityComparer<T>.Default.Equals(value, default(T));
	}
}
