using System;
using System.ComponentModel;
using System.Windows;
using TraverseTree.Core.Models;

namespace TraverseTree.Visual.Models
{
	public class VisualBinaryTreeNode<TKey, TValue> : BinaryTreeNode<TKey, TValue>, INotifyPropertyChanged
		where TKey: IComparable<TKey>
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public int Radius
		{
			get
			{
				return _radius;
			}
			set
			{
				if (_radius != value)
				{
					_radius = value;

					OnPropertyChanged(nameof(Radius));
				}
			}
		}

		public VisualTreeNodeType TreeNodeType
		{
			get { return _mode; }
			set
			{
				if (_mode != value)
				{
					_mode = value;
					OnPropertyChanged(nameof(TreeNodeType));
				}
			}
		}

		public int HeightOffset => Radius / 2;
		public int WidthOffset => Radius / 2;

		public Point Center =>
			new Point(( 1.5 * _x + 0.5 ) * Radius, ( 1.5 * _y + 0.5 ) * Radius);

		public Point From =>
			new Point(Center.X, Center.Y + Radius);

		public Point ToRight =>
			new Point(Center.X + 2.5 * Radius, Center.Y + 2.5 * Radius);

		public Point ToLeft =>
			new Point(Center.X - 2.5 * Radius, Center.Y + 2.5 * Radius);

		public VisualBinaryTreeNode(TKey key, TValue value, int radius = 40) :
			base(key, value)
		{
			_radius = radius;
		}

		protected virtual void OnPropertyChanged(string property) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

		private int _radius;
		private VisualTreeNodeType _mode;
	}
}
