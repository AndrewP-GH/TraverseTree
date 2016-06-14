using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TraverseTree.Visual.Converters
{
	internal sealed class WidthConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			double actualHeight = (double)values[0];
			int maximumHeight = (int)values[1],
				currentHeight = (int)values[2];

			if (maximumHeight == 0) {
				return 0;
			}

			double step = actualHeight / maximumHeight;
			return currentHeight * step;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;
	}
}
