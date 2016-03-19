using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TraverseTree.Core.Abstract;
using TraverseTree.Core.Extensions;
using TraverseTree.Core.Models;
using TraverseTree.Visual.Abstract;

namespace TraverseTree.Visual.ViewModels
{
	/// <summary>
	/// 
	/// </summary>
	public class MainViewModel : BaseViewModel
	{
		/// <summary>
		/// 
		/// </summary>
		public ICommand CloseCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICommand AboutCommand { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public ICommand GenerateTreeCommand { get; set; }


		/// <summary>
		/// 
		/// </summary>
		public TraverseMode TraverseOrder
		{
			get { return _traverseOrder; }
			set
			{
				if (_traverseOrder != value)
				{
					_traverseOrder = value;
					base.OnPropertyChanged(nameof(TraverseOrder));
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public MainViewModel()
		{
			
		}

		protected void CreateNode ()
		{
			// Create Visual Node
			// Insert node to tree
			// Add node to observable collection
		}

		protected void TraverseTree ()
		{
			// Create visitor with observable stack
			// foreach ()
			// 
		}

		private TraverseMode _traverseOrder;
	}
}
