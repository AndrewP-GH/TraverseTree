using System;

namespace TraverseTree.Visual.Abstract
{
	public interface IActionManager
	{
		bool Executing { get; }

		void RegisterAction(Action action);
		void StartExecutingActions();
		void StopExecutingActions();
	}
}
