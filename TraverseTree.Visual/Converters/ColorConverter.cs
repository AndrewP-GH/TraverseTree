using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

using TraverseTree.Core.Extensions;
using TraverseTree.Visual.Models;

namespace TraverseTree.Visual.Converters
{
	internal sealed class TreeNodeColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.IsNull()) {
				throw new ArgumentNullException(nameof(value));
			}

			Color color = default(Color);
			VisualTreeNodeType nodeType = (VisualTreeNodeType)value;

			switch (nodeType)
			{
				case VisualTreeNodeType.Hidden:
					color = Colors.White;
					break;
				case VisualTreeNodeType.InsertedForTraverse:
					color = Colors.YellowGreen;
					break;
				case VisualTreeNodeType.Active:
					color = Colors.MediumVioletRed;
					break;
				case VisualTreeNodeType.InsertedToTree:
					color = Colors.Green;
					break;
			}

			return new SolidColorBrush(color);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			value.NullGuard(nameof(value));

			SolidColorBrush brush = (SolidColorBrush)value;

			if (brush.IsNull()) {
				throw new ArgumentException("Invalid value");
			}

			return brush.Color == Colors.White ? VisualTreeNodeType.Hidden :
				brush.Color == Colors.YellowGreen ? VisualTreeNodeType.InsertedForTraverse :
				brush.Color == Colors.MediumVioletRed ? VisualTreeNodeType.Active : VisualTreeNodeType.InsertedToTree;
		}
	}
}
