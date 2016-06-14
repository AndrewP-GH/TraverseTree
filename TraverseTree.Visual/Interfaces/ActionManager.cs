using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TraverseTree.Visual.Abstract;
using TraverseTree.Core.Extensions;

namespace TraverseTree.Visual.Interfaces
{
	public class ActionManager : IActionManager
	{
		public TimeSpan Interval =>
			_timer.Interval;

		public bool Executing =>
			_timer.IsEnabled;

		public ActionManager()
		{
			_actions = new Queue<Action>();

			_timer = new DispatcherTimer(DispatcherPriority.Render){
				Interval = DefaultInterval
			};
			_timer.Tick += OnExecutingAction;
		}

		public void RegisterAction(Action action) =>
			_actions.Enqueue(action);

		public void StartExecutingActions() =>
			_timer.Start();

		public void StopExecutingActions() =>
			_timer.Stop();

		protected void OnExecutingAction (object sender, EventArgs args)
		{
			if (_actions.Count == 0) {
				StopExecutingActions();
			} else {
				_actions.Dequeue().Invoke();
			}
		}

		private readonly Queue<Action> _actions;
		private readonly DispatcherTimer _timer;
		private static readonly TimeSpan DefaultInterval = TimeSpan.FromMilliseconds(600);
	}
}
