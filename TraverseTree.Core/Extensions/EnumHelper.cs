using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace TraverseTree.Core.Extensions
{
	public static class EnumHelper
	{
		public static object ParseByDescription(string target, Type type)
		{
			target.NullGuard(nameof(target));

			var pairs = Enum.GetValues(type)
				.Cast<object>()
				.Select(x => new KeyValuePair<string, Enum>(DescriptionCore(type, x.ToString()), (Enum)x));

			foreach(var pair in pairs)
			{
				if (String.Compare(pair.Key, target) == 0) {
					return pair.Value;
				}
			}

			throw new ArgumentException("Invalid parameter", nameof(target));
		}

		public static string Description(string value, Type enumType)
		{
			value.NullGuard(nameof(value));
			return DescriptionCore(enumType, value);
		}

		public static string Description<TEnum>(string value) where TEnum : struct
		{
			value.NullGuard(nameof(value));
			return DescriptionCore(typeof(TEnum), value);
		}

		public static string Description<TEnum>(TEnum value) where TEnum : struct =>
			DescriptionCore(typeof(TEnum), value.ToString());

		public static string[] Descriptions<TEnum>(int skipCount = 0) where TEnum : struct
		{
			Type type = typeof(TEnum);

			return Enum.GetValues(type)
				.Cast<TEnum>()
					.Select(x => DescriptionCore(type, x.ToString()))
					.ToArray();
		}

		private static string DescriptionCore(Type type, string value)
		{
			FieldInfo info = type.GetField(value);

			DescriptionAttribute[] descriptions =
				info.GetCustomAttributes<DescriptionAttribute>().ToArray();

			if (descriptions != null && descriptions.Length > 0)
			{
				return descriptions[0].Description;
			}

			return value;
		}
	}
}
