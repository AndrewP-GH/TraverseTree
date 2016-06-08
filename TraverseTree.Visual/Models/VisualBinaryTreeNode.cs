using System;
using System.ComponentModel;
using System.Windows;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Models;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Visual.Models
{
	public class VisualBinaryTreeNode<TKey, TValue> : BinaryTreeNode<TKey, TValue>, INotifyPropertyChanged
		where TKey: IComparable<TKey>
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public VisualTreeNodeType TreeNodeType
		{
			get { return _type; }
			set
			{
				if (_type != value)
				{
					_type = value;
					OnPropertyChanged(nameof(TreeNodeType));
				}
			}
		}

		public int Diameter
		{
			get
			{
				return _radius * 2;
			}
			set
			{
				if (_radius != value)
				{
					_radius = value / 2;

					OnPropertyChanged(nameof(Diameter));
				}
			}
		}

		public override BinaryTreeNode<TKey, TValue> Parent
		{
			get { return base.Parent; }

			set
			{
				if (value.IsNull())
				{
					_x = _y = 0;
				} else
				{
					if (value.GetType() != this.GetType()) {
						throw new ArgumentException("Invalid Type");
					}
					ChangeIndex((VisualBinaryTreeNode<TKey, TValue>)value);
				}
				base.Parent = value;
			}
		}

		public decimal XPos =>
			2 * _x * _radius + 600;

		public decimal CoordX =>
			2 * _x * _radius;

		public decimal CoordY =>
			3 * _y * _radius;

		public decimal BeginEdgeX =>
			_x + _radius;

		public decimal BeginEdgeY =>
			_y + 2 * _radius;

		public decimal EndLeftEdgeX =>
			_x - _radius;

		public decimal EndRightEdgeX =>
			_x + 3 * _radius;

		public decimal EndEdgeY =>
			_y + 3 * _radius;

		public VisualBinaryTreeNode(TKey key, TValue value) : 
			this(key, value, DefaultRadius) { }

		public VisualBinaryTreeNode(TKey key, TValue value, int radius) :
			base(key, value)
		{
			if (radius <= 0) {
				throw new ArgumentException("Must be positive", nameof(radius));
			}

			_radius = radius;
			_type = VisualTreeNodeType.Hidden;
		}

		protected virtual void OnPropertyChanged(string property) =>
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

		private void ChangeIndex(VisualBinaryTreeNode<TKey, TValue> parent)
		{
			_y = 1 + parent._y;

			if (_y == 1)
			{
				_x = parent.IsLeftChild(this) ? -1 : 1;
			} else
			{
				int x = parent._x * 2;

				_x = x > 0 ?
					( Parent.IsLeftChild(this) ? x - 1 : x ) :
					( Parent.IsRightChild(this) ? x + 1 : x );
			}
		}

		private int _x;
		private int _y;
		private int _radius;
		private VisualTreeNodeType _type;
		private const int DefaultRadius = 10;
	}
}
