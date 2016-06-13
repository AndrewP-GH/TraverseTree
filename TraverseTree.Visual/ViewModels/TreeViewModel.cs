using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

using TraverseTree.Visual.Abstract;
using TraverseTree.Visual.Models;
using TraverseTree.Core.Extensions;
using TraverseTree.Core.Models;

namespace TraverseTree.Visual.ViewModels
{
	using Node = BinaryTreeNode<int, ViewData>;

	public class TreeViewModel : ObservableObject
	{
		public int Radius { get; } = 20;
		public int Diameter => Radius * 2;

		public int PreferedWidth
		{
			get { return _preferedWidth; }
			set { UpdateValue(ref _preferedWidth, value, nameof(PreferedWidth)); }
		}
		public int PreferedHeight
		{
			get { return _preferedHeight; }
			set { UpdateValue(ref _preferedHeight, value, nameof(PreferedHeight)); }
		}

		public double ScaleX
		{
			get { return _scaleX; }
			set { UpdateValue(ref _scaleX, value, nameof(ScaleX)); }
		}
		public double ScaleY
		{
			get { return _scaleY; }
			set { UpdateValue(ref _scaleY, value, nameof(ScaleY)); }
		}

		public ObservableCollection<ViewData> Collection => _nodes;

		public TreeViewModel(IActionManager manager)
		{
			manager.NullGuardAssign(out _manager, nameof(manager));
			_nodes = new ObservableCollection<ViewData>();
			_nodes.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);
		}

		public void UpdateState(BinarySearchTree<int, ViewData> tree)
		{
			var enumerator = tree.GetEnumerator(TraverseMode.Leverorder)
				.ToArray();

			int maxLevel = enumerator.Last().Level;
			int maxNodeCount = ( 1 << maxLevel );

			PreferedWidth = maxNodeCount * 2 * Diameter;
			PreferedHeight = ( maxLevel + 2 ) * 2 * Diameter;

			var groups = enumerator
				.GroupBy(x => x.Level)
				.ToArray();

			foreach (var group in groups)
			{
				int diff = maxLevel - group.Key;

				foreach (var node in group)
				{
					node.Value.UpdateVisualState(
						left: PositionOffset(diff) + Diameter * node.LevelOrder + node.LevelOrder * NodeOffset(diff),
						top: Diameter + 2 * node.Level * Diameter,
						radius: Radius,
						offset: NodeOffset(diff - 1),
						style: Style(node)
					);

					_nodes.Add(node.Value);
				}
			}
		}

		public void UpdateScale(bool more)
		{
			if (more)
			{
				ScaleX *= ScaleRate;
				ScaleY *= ScaleRate;
			} else
			{
				ScaleX /= ScaleRate;
				ScaleY /= ScaleRate;
			}
		}

		public void Clear() => _nodes.Clear();

		protected void OnCollectionChanged (object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.Action == NotifyCollectionChangedAction.Add)
			{
				if (args.NewItems != null && args.NewItems.Count > 0)
				{
					_manager.RegisterAction(
						() => ( (ViewData)args.NewItems[0] ).VisualType = VisualTreeNodeType.InsertedToTree
					);
				}
			} 
		}

		private int NodeOffset(int levelDifference) =>
			levelDifference < 0 ? -1 : ( ( 1 << ( levelDifference + 1 ) ) - 1 ) * Diameter;

		private int PositionOffset(int levelDifference) =>
			Radius + ( ( 1 << levelDifference ) - 1 ) * Diameter;

		private ChildDrawStyle Style (Node node)
		{
			if (node.IsLeaf)
				return ChildDrawStyle.None;

			if (node.Left.IsNull())
				return ChildDrawStyle.Right;

			if (node.Right.IsNull())
				return ChildDrawStyle.Left;

			return ChildDrawStyle.Both;
		}

		private int _preferedWidth;
		private int _preferedHeight;
		private double _scaleX = 1.0;
		private double _scaleY = 1.0;
		private const double ScaleRate = 1.1;
		private readonly IActionManager _manager;
		private readonly ObservableCollection<ViewData> _nodes;
	}
}
