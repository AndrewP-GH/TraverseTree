using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraverseTree.Core.Models
{
	/// <summary>
	/// Default BST node comparer
	/// </summary>
	/// <typeparam name="T"><seealso cref="IComparable{T}"/></typeparam>
	public class DefaultNodeComparer<T> : IComparer<T> where T : IComparable<T>
	{
		/// <summary>
		/// Compare beetween two values.
		/// </summary>
		/// <param name="x">Left value</param>
		/// <param name="y">Right value</param>
		/// <returns></returns>
		public int Compare(T x, T y) => x.CompareTo(y);

		private static DefaultNodeComparer<T> _defaultComparer;
		public static DefaultNodeComparer<T> Comparer =>
			_defaultComparer ?? ( _defaultComparer = new DefaultNodeComparer<T>() );
	}

	/// <summary>
	/// Default BST value comparer
	/// </summary>
	/// <typeparam name="T">Value type of BST node</typeparam>
	public class DefaultValueComparer<T> : IEqualityComparer<T>
	{
		/// <summary>
		/// Determine if x and y are same. Only check HashCode of each vales
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>True if hash code's are equal. Otherwise - false.</returns>
		public bool Equals(T x, T y) =>
			GetHashCode(x) == GetHashCode(y);

		public int GetHashCode(T obj)
		{
			if (Object.ReferenceEquals(obj, null))
				throw new ArgumentNullException(nameof(obj));

			return obj.GetHashCode();
		}
	}

	public class EqutableValueComparer<T> : IEqualityComparer<T> where T : IEquatable<T>
	{
		public bool Equals(T x, T y) =>
			x.Equals(y);

		public int GetHashCode(T obj)
		{
			if (Object.ReferenceEquals(obj, null))
				throw new ArgumentNullException(nameof(obj));

			return obj.GetHashCode();
		}
	}
}
