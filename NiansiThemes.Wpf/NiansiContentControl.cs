using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NiansiThemes.Wpf
{
    class NiansiContentControl : ContentControl
    {
        public static readonly DependencyProperty ReverseTransitionProperty =
            DependencyProperty.Register("ReverseTransition", typeof(bool), typeof(NiansiContentControl), new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty TransitionsEnabledProperty =
            DependencyProperty.Register("TransitionsEnabled", typeof(bool), typeof(NiansiContentControl), new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty OnlyLoadTransitionProperty =
            DependencyProperty.Register("OnlyLoadTransition", typeof(bool), typeof(NiansiContentControl), new FrameworkPropertyMetadata(false));

        public static readonly RoutedEvent TransitionCompletedEvent =
            EventManager.RegisterRoutedEvent("TransitionCompleted", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NiansiContentControl));

        public bool ReverseTransition
        {
            get { return (bool)GetValue(ReverseTransitionProperty); }
            set { SetValue(ReverseTransitionProperty, value); }
        }

        public bool TransitionsEnabled
        {
            get { return (bool)GetValue(TransitionsEnabledProperty); }
            set { SetValue(TransitionsEnabledProperty, value); }
        }

        public bool OnlyLoadTransition
        {
            get { return (bool)GetValue(OnlyLoadTransitionProperty); }
            set { SetValue(OnlyLoadTransitionProperty, value); }
        }

        public event RoutedEventHandler TransitionCompleted
        {
            add { AddHandler(TransitionCompletedEvent, value); }
            remove { RemoveHandler(TransitionCompletedEvent, value); }
        }

        private Storyboard _afterLoadedStoryboard;
        private Storyboard _afterLoadedReverseStoryboard;
        private bool _transitionLoaded;

        public NiansiContentControl()
        {
            DefaultStyleKey = typeof(NiansiContentControl);

            Loaded += MetroContentControlLoaded;
            Unloaded += MetroContentControlUnloaded;
        }

        void MetroContentControlIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (TransitionsEnabled && !_transitionLoaded)
            {
                if (!IsVisible)
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                else
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
            }
        }

        private void MetroContentControlUnloaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                UnsetStoryboardEvents();
                if (_transitionLoaded)
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterUnLoadedReverse" : "AfterUnLoaded", false);
                IsVisibleChanged -= MetroContentControlIsVisibleChanged;
            }
        }

        private void MetroContentControlLoaded(object sender, RoutedEventArgs e)
        {
            if (TransitionsEnabled)
            {
                if (!_transitionLoaded)
                {
                    SetStoryboardEvents();
                    _transitionLoaded = OnlyLoadTransition;
                    VisualStateManager.GoToState(this, ReverseTransition ? "AfterLoadedReverse" : "AfterLoaded", true);
                }
                IsVisibleChanged -= MetroContentControlIsVisibleChanged;
                IsVisibleChanged += MetroContentControlIsVisibleChanged;
            }
            else
            {
                var root = (Grid)GetTemplateChild("root");
                if (root != null)
                {
                    root.Opacity = 1.0;
                    var transform = ((System.Windows.Media.TranslateTransform)root.RenderTransform);
                    if (transform.IsFrozen)
                    {
                        var modifiedTransform = transform.Clone();
                        modifiedTransform.X = 0;
                        root.RenderTransform = modifiedTransform;
                    }
                    else
                    {
                        transform.X = 0;
                    }
                }
            }
        }

        public void Reload()
        {
            if (!TransitionsEnabled || _transitionLoaded) return;

            if (ReverseTransition)
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterUnLoadedReverse", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "BeforeLoaded", true);
                VisualStateManager.GoToState(this, "AfterLoaded", true);
            }

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _afterLoadedStoryboard = GetTemplateChild("AfterLoadedStoryboard") as Storyboard;
            _afterLoadedReverseStoryboard = GetTemplateChild("AfterLoadedReverseStoryboard") as Storyboard;
        }

        private void AfterLoadedStoryboardCompleted(object sender, System.EventArgs e)
        {
            if (_transitionLoaded)
                UnsetStoryboardEvents();
            InvalidateVisual();
            RaiseEvent(new RoutedEventArgs(TransitionCompletedEvent));
        }

        private void SetStoryboardEvents()
        {
            if (_afterLoadedStoryboard != null)
                _afterLoadedStoryboard.Completed += AfterLoadedStoryboardCompleted;
            if (_afterLoadedReverseStoryboard != null)
                _afterLoadedReverseStoryboard.Completed += AfterLoadedStoryboardCompleted;
        }

        private void UnsetStoryboardEvents()
        {
            if (_afterLoadedStoryboard != null)
                _afterLoadedStoryboard.Completed -= AfterLoadedStoryboardCompleted;
            if (_afterLoadedReverseStoryboard != null)
                _afterLoadedReverseStoryboard.Completed -= AfterLoadedStoryboardCompleted;
        }
    }
}
