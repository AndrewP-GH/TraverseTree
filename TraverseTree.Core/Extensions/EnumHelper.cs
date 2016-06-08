using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TraverseTree.Core.Extensions
{
	public static class EnumHelper
	{
		public static string Description<TEnum>(string value) where TEnum : struct
		{
			FieldInfo info = typeof(TEnum).GetField(value);

			DescriptionAttribute[] descriptions =
				info.GetCustomAttributes<DescriptionAttribute>().ToArray();

			if (descriptions != null && descriptions.Length > 0)
			{
				return descriptions[0].Description;
			}

			return value;
		}

		public static string Description<TEnum>(TEnum value) where TEnum : struct
		{
			return Description<TEnum>(
				value.ToString()
			);
		}

		public static string[] Descriptions<TEnum>(int skipCount = 0) where TEnum : struct
		{
			return Enum.GetValues(typeof(TEnum))
				.Cast<TEnum>()
					.Select(Description)
					.ToArray();
		}
	}
}
