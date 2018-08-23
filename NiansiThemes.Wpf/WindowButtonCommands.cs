using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NiansiThemes.Wpf
{
    [TemplatePart(Name = MIN_BUTTON, Type = typeof(Button))]
    [TemplatePart(Name = MAX_BUTTON, Type = typeof(Button))]
    [TemplatePart(Name = CLOSE_BUTTON, Type = typeof(Button))]
    public class WindowButtonCommands : ContentControl, INotifyPropertyChanged
    {
        private const string MIN_BUTTON = "MinButton";
        private const string MAX_BUTTON = "MaxButton";
        private const string CLOSE_BUTTON = "CloseButton";

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private NiansiWindow _parentWindow;

        public NiansiWindow ParentWindow
        {
            get => _parentWindow;
            set
            {
                if (Equals(_parentWindow, value)) return;
                _parentWindow = value;
                this.RaisePropertyChanged("ParentWindow");
            }
        }

        public static readonly DependencyProperty MinimizeProperty =
            DependencyProperty.Register("Minimize", typeof(string), typeof(WindowButtonCommands), new PropertyMetadata(null));

        public static readonly DependencyProperty MaximizeProperty =
            DependencyProperty.Register("Maximize", typeof(string), typeof(WindowButtonCommands), new PropertyMetadata(null));

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.Register("Close", typeof(string), typeof(WindowButtonCommands), new PropertyMetadata(null));

        /// <summary>
        /// 获取/设置最小化按钮文字提示
        /// </summary>
        public string Minimize
        {
            get => (string)GetValue(MinimizeProperty);
            set => SetValue(MinimizeProperty, value);
        }

        /// <summary>
        /// 获取/设置最大化按钮文字提示
        /// </summary>
        public string Maximize
        {
            get => (string)GetValue(MaximizeProperty);
            set => SetValue(MaximizeProperty, value);
        }

        /// <summary>
        /// 获取/设置关闭按钮文字提示
        /// </summary>
        public string Close
        {
            get => (string)GetValue(CloseProperty);
            set => SetValue(CloseProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var minButton = Template.FindName(MIN_BUTTON, this) as Button;
            if (minButton != null) minButton.Click += MinimizeClick;

            var maxButton = Template.FindName(MAX_BUTTON, this) as Button;
            if (maxButton != null) maxButton.Click += MaximizeClick;

            var closeButton = Template.FindName(CLOSE_BUTTON, this) as Button;
            if (closeButton != null) closeButton.Click += CloseClick;
        }

        private void MinimizeClick(object sender, RoutedEventArgs e)
        {
            if (ParentWindow == null) return;
            SystemCommands.MinimizeWindow(ParentWindow);
        }

        private void MaximizeClick(object sender, RoutedEventArgs e)
        {
            if (ParentWindow == null) return;
            if (ParentWindow.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(ParentWindow);
            else
                SystemCommands.MaximizeWindow(ParentWindow);
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            var closingWindowEventHandlerArgs = new ClosingWindowEventHandlerArgs();
            OnClosingWindow(closingWindowEventHandlerArgs);

            if (closingWindowEventHandlerArgs.Cancelled) return;

            if (ParentWindow == null) return;
            ParentWindow.Close();
        }

        public event ClosingWindowEventHandler ClosingWindow;
        public delegate void ClosingWindowEventHandler(object sender, ClosingWindowEventHandlerArgs args);
        protected void OnClosingWindow(ClosingWindowEventHandlerArgs args)
        {
            var handler = ClosingWindow;
            if (handler != null) handler(this, args);
        }
    }
}
