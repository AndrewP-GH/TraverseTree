using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace TraverseTree.Visual.Interfaces
{
	public class BindableDoubleAnimation : DoubleAnimationBase
	{
		public DoubleAnimation InternalAnimation { get { return _animation; } }

		public double To
		{
			get { return (double)GetValue(ToProperty); }
			set { SetValue(ToProperty, value); }
		}

		public static readonly DependencyProperty ToProperty =
			DependencyProperty.Register("To", typeof(double), typeof(BindableDoubleAnimation), new UIPropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
			{
				BindableDoubleAnimation sender = (BindableDoubleAnimation)s;
				sender._animation.To = (double)e.NewValue;
			})));


		public double From
		{
			get { return (double)GetValue(FromProperty); }
			set { SetValue(FromProperty, value); }
		}

		public static readonly DependencyProperty FromProperty =
			DependencyProperty.Register("From", typeof(double), typeof(BindableDoubleAnimation), new UIPropertyMetadata(0d, new PropertyChangedCallback((s, e) =>
			{
				BindableDoubleAnimation sender = (BindableDoubleAnimation)s;
				sender._animation.From = (double)e.NewValue;
			})));


		public BindableDoubleAnimation()
		{
			_animation = new DoubleAnimation();
		}

		protected override double GetCurrentValueCore(double defaultOriginValue, double defaultDestinationValue, AnimationClock animationClock) =>
			_animation.GetCurrentValue(defaultOriginValue, defaultDestinationValue, animationClock);

		protected override Freezable CreateInstanceCore() =>
			_animation.Clone();

		private readonly DoubleAnimation _animation;
	}
}
