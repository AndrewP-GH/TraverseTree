using System;
using TraverseTree.Visual.Abstract;

namespace TraverseTree.Visual.Models
{
	internal enum ChildDrawStyle : byte
	{
		None = 0,
		Left = 0x01,
		Right = 0x10,
		Both = 0x11
	}

	public class ViewData : ObservableObject
	{
		public VisualTreeNodeType VisualType
		{
			get { return _type; }
			set { UpdateValue(ref _type, value, nameof(VisualType)); }
		}

		public double Diameter { get; private set; }

		public double PositionX { get; private set; }
		public double PositionY { get; private set; }

		public double BeginEdgeX { get; private set; }
		public double BeginEdgeY { get; private set; }
		public double EndLeftEdgeX { get; private set; }
		public double EndRightEdgeX { get; private set; }
		public double EndEdgeY { get; private set; }

		public double LeftThickness =>
			_style.HasFlag(ChildDrawStyle.Left) ? 3 : 0 ;
		public double RightThickness =>
			_style.HasFlag(ChildDrawStyle.Right) ? 3 : 0;

		public ViewData()
		{
			VisualType = VisualTreeNodeType.InsertedToTree;
			EndLeftEdgeX = Double.NaN;
			EndRightEdgeX = Double.NaN;
			EndEdgeY = Double.NaN;
		}

		internal void UpdateVisualState (int left, int top, int radius, int offset, ChildDrawStyle style)
		{
			PositionX = left;
			PositionY = top;
			Diameter = radius * 2.0f;
			BeginEdgeX = left + radius;
			BeginEdgeY = top + Diameter;
			EndLeftEdgeX = left - offset / 2;
			EndRightEdgeX = left + offset / 2 + Diameter;
			EndEdgeY = BeginEdgeY + Diameter;

			_style = style;
		}

		private ChildDrawStyle _style;
		private VisualTreeNodeType _type;
	}
}
