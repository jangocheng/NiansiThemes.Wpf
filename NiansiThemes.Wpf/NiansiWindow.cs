using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace NiansiThemes.Wpf
{
    [TemplatePart(Name = WINDOW_TITLE_BAR, Type = typeof(UIElement))]
    [TemplatePart(Name = WINDOW_ICON, Type = typeof(UIElement))]
    [TemplatePart(Name = WINDOW_TITLE, Type = typeof(UIElement))]
    [TemplatePart(Name = LEFT_WINDOW_COMMANDS, Type = typeof(WindowCommands))]
    [TemplatePart(Name = RIGHT_WINDOW_COMMANDS, Type = typeof(WindowCommands))]
    [TemplatePart(Name = WINDOW_BUTTON_COMMANDS, Type = typeof(WindowButtonCommands))]
    [TemplatePart(Name = HAMBURGER_MENU_BOX, Type = typeof(HamburgerMenuBox))]
    [TemplatePart(Name = NIANSI_CONTENT_CONTROL, Type = typeof(NiansiContentControl))]
    public class NiansiWindow : Window
    {
        private const string WINDOW_TITLE_BAR = "WindowTitleBar";
        private const string WINDOW_ICON = "WindowIcon";
        private const string WINDOW_TITLE = "WindowTitle";
        private const string LEFT_WINDOW_COMMANDS = "LeftWindowCommands";
        private const string RIGHT_WINDOW_COMMANDS = "RightWindowCommands";
        private const string WINDOW_BUTTON_COMMANDS = "WindowButtonCommands";
        private const string HAMBURGER_MENU_BOX = "HamburgerMenuBox";
        private const string NIANSI_CONTENT_CONTROL = "NiansiContentControl";

        public static readonly DependencyProperty IconTemplateProperty =
           DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(NiansiWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty WindowTitleTemplateProperty =
            DependencyProperty.Register("WindowTitleTemplate", typeof(DataTemplate), typeof(NiansiWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty WindowTitleForegroundProperty =
            DependencyProperty.Register("WindowTitleForeground", typeof(Brush), typeof(NiansiWindow));

        public static readonly DependencyProperty WindowTitleBarBackgroundProperty =
            DependencyProperty.Register("WindowTitleBarBackground", typeof(Brush), typeof(NiansiWindow), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty WindowTitleBarHeightProperty =
            DependencyProperty.Register("WindowTitleBarHeight", typeof(int), typeof(NiansiWindow), new PropertyMetadata(null));

        public static readonly DependencyProperty HamburgerMenuBackgroundProperty =
            DependencyProperty.Register("HamburgerMenuBackground", typeof(Brush), typeof(NiansiWindow), new PropertyMetadata(Brushes.Transparent));

        public static readonly DependencyProperty WindowTransitionsEnabledProperty =
            DependencyProperty.Register("WindowTransitionsEnabled", typeof(bool), typeof(NiansiWindow), new PropertyMetadata(true));

        public static readonly DependencyProperty LeftWindowCommandsProperty =
            DependencyProperty.Register("LeftWindowCommands", typeof(WindowCommands), typeof(NiansiWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        public static readonly DependencyProperty RightWindowCommandsProperty =
            DependencyProperty.Register("RightWindowCommands", typeof(WindowCommands), typeof(NiansiWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        public static readonly DependencyProperty WindowButtonCommandsProperty =
            DependencyProperty.Register("WindowButtonCommands", typeof(WindowButtonCommands), typeof(NiansiWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        public static readonly DependencyProperty HamburgerMenuBoxProperty =
            DependencyProperty.Register("HamburgerMenuBox", typeof(HamburgerMenuBox), typeof(NiansiWindow), new PropertyMetadata(null, UpdateLogicalChilds));

        /// <summary>
        /// Gets/sets whether the window's entrance transition animation is enabled.
        /// </summary>
        public bool WindowTransitionsEnabled
        {
            get { return (bool)this.GetValue(WindowTransitionsEnabledProperty); }
            set { SetValue(WindowTransitionsEnabledProperty, value); }
        }

        public WindowCommands LeftWindowCommands
        {
            get { return (WindowCommands)GetValue(LeftWindowCommandsProperty); }
            set { SetValue(LeftWindowCommandsProperty, value); }
        }

        public WindowCommands RightWindowCommands
        {
            get { return (WindowCommands)GetValue(RightWindowCommandsProperty); }
            set { SetValue(RightWindowCommandsProperty, value); }
        }

        /// <summary>
        /// Gets/sets the window button commands that hosts the min/max/close commands.
        /// </summary>
        public WindowButtonCommands WindowButtonCommands
        {
            get { return (WindowButtonCommands)GetValue(WindowButtonCommandsProperty); }
            set { SetValue(WindowButtonCommandsProperty, value); }
        }

        /// <summary>
        /// 获取/设置图标内容模板以显示自定义图标。
        /// </summary>
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets the title content template to show a custom title.
        /// </summary>
        public DataTemplate WindowTitleTemplate
        {
            get { return (DataTemplate)GetValue(WindowTitleTemplateProperty); }
            set { SetValue(WindowTitleTemplateProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the titlebar's foreground.
        /// </summary>
        public Brush WindowTitleForeground
        {
            get { return (Brush)GetValue(WindowTitleForegroundProperty); }
            set { SetValue(WindowTitleForegroundProperty, value); }
        }

        /// <summary>
        /// Gets/sets the brush used for the Window's title bar.
        /// </summary>
        public Brush WindowTitleBarBackground
        {
            get { return (Brush)GetValue(WindowTitleBarBackgroundProperty); }
            set { SetValue(WindowTitleBarBackgroundProperty, value); }
        }

        public int WindowTitleBarHeight
        {
            get { return (int)GetValue(WindowTitleBarHeightProperty); }
            set { SetValue(WindowTitleBarHeightProperty, value); }
        }

        public Brush HamburgerMenuBackground
        {
            get { return (Brush)GetValue(HamburgerMenuBackgroundProperty); }
            set { SetValue(HamburgerMenuBackgroundProperty, value); }
        }

        /// <summary>
        /// 获取/设置菜单内容模板以显示左侧菜单。
        /// </summary>
        public HamburgerMenuBox HamburgerMenuBox
        {
            get { return (HamburgerMenuBox)GetValue(HamburgerMenuBoxProperty); }
            set { SetValue(HamburgerMenuBoxProperty, value); }
        }

        static NiansiWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NiansiWindow), new FrameworkPropertyMetadata(typeof(NiansiWindow)));
        }

        public NiansiWindow()
        {
            Loaded += NiansiWindow_Loaded;
            DataContextChanged += NiansiWindow_DataContextChanged;
        }

        private void NiansiWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.WindowTransitionsEnabled)
            {
                VisualStateManager.GoToState(this, "AfterLoaded", true);
            }
        }

        private void NiansiWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (LeftWindowCommands != null) LeftWindowCommands.DataContext = DataContext;
            if (RightWindowCommands != null) RightWindowCommands.DataContext = DataContext;
            if (WindowButtonCommands != null) WindowButtonCommands.DataContext = DataContext;
            if (HamburgerMenuBox != null) HamburgerMenuBox.DataContext = DataContext;
        }

        private static void UpdateLogicalChilds(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var window = dependencyObject as NiansiWindow;
            if (window == null)
            {
                return;
            }
            var oldChild = e.OldValue as FrameworkElement;
            if (oldChild != null)
            {
                window.RemoveLogicalChild(oldChild);
            }
            var newChild = e.NewValue as FrameworkElement;
            if (newChild != null)
            {
                window.AddLogicalChild(newChild);
                // Yes, that's crazy. But we must do this to enable all possible scenarios for setting DataContext
                // in a Window. Without set the DataContext at this point it can happen that e.g. a Flyout
                // doesn't get the same DataContext.
                // So now we can type
                //
                // this.InitializeComponent();
                // this.DataContext = new MainViewModel();
                //
                // or
                //
                // this.DataContext = new MainViewModel();
                // this.InitializeComponent();
                //
                newChild.DataContext = window.DataContext;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (LeftWindowCommands == null) LeftWindowCommands = new WindowCommands();
            if (RightWindowCommands == null) RightWindowCommands = new WindowCommands();
            if (WindowButtonCommands == null) WindowButtonCommands = new WindowButtonCommands();
            if (HamburgerMenuBox == null) HamburgerMenuBox = new HamburgerMenuBox();

            WindowButtonCommands.ParentWindow = this;

            var windowTitleBar = GetTemplateChild(WINDOW_TITLE_BAR) as UIElement;
            if (windowTitleBar != null)
            {
                windowTitleBar.MouseMove += NiansiWindowTitleBar_MouseMove;
            }
        }

        private void NiansiWindowTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }
    }
}
