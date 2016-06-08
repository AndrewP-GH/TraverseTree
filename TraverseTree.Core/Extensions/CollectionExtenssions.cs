using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TraverseTree.Core.Extensions
{
	public static class CollectionExtennsions
	{
		public static bool IsEmpty(this ICollection collection) => collection.Count == 0;
	}
}
