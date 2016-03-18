using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraverseTree.Core.Abstract
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="THierarchical"></typeparam>
	public interface IBinaryHierarchical<THierarchical> where THierarchical : IBinaryHierarchical<THierarchical>
	{
		/// <summary>
		/// 
		/// </summary>
		THierarchical Left { get; set; }

		/// <summary>
		/// 
		/// </summary>
		THierarchical Right { get; set; }

		/// <summary>
		/// 
		/// </summary>
		THierarchical Parent { get; set; }
	}
}
