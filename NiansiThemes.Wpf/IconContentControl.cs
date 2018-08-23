using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NiansiThemes.Wpf
{
    /// <summary>
    /// 显示图标和内容为主题格式的控件
    /// </summary>
    [TemplatePart(Name = ICON, Type = typeof(UIElement))]
    [TemplatePart(Name = TITLE, Type = typeof(UIElement))]
    public class IconContentControl : ContentControl
    {
        private const string ICON = "Icon";
        private const string TITLE = "Title";

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(int), typeof(IconContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(int), typeof(IconContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IconGeometryDataProperty =
            DependencyProperty.Register("IconGeometryData", typeof(string), typeof(IconContentControl), new PropertyMetadata(null));

        public static readonly DependencyProperty IconEnabledProperty =
            DependencyProperty.Register("IconEnabled", typeof(bool), typeof(IconContentControl), new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty ContentEnabledProperty =
            DependencyProperty.Register("ContentEnabled", typeof(bool), typeof(IconContentControl), new FrameworkPropertyMetadata(true));

        public static readonly DependencyProperty SeparatorProperty =
            DependencyProperty.Register("Separator", typeof(float), typeof(IconContentControl), new PropertyMetadata(5f));

        public static readonly DependencyProperty PersistProperty =
            DependencyProperty.Register("Persist", typeof(float), typeof(IconContentControl), new PropertyMetadata(0f));

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IconContentControl));

        /// <summary>
        /// 获取/设置显示Icon的宽度
        /// </summary>
        public int IconWidth
        {
            get => (int)GetValue(IconWidthProperty);
            set => SetValue(IconWidthProperty, value);
        }

        /// <summary>
        /// 获取/设置显示Icon的高度
        /// </summary>
        public int IconHeight
        {
            get => (int)GetValue(IconHeightProperty);
            set => SetValue(IconHeightProperty, value);
        }

        /// <summary>
        /// 获取/设置显示Icon的SVG格式的数据
        /// </summary>
        public string IconGeometryData
        {
            get => (string)GetValue(IconGeometryDataProperty);
            set => SetValue(IconGeometryDataProperty, value);
        }

        /// <summary>
        /// 获取/设置Icon是否显示
        /// </summary>
        public bool IconEnabled
        {
            get => (bool)GetValue(IconEnabledProperty);
            set => SetValue(IconEnabledProperty, value);
        }

        /// <summary>
        /// 获取/设置Icon与Content之间的间隔
        /// </summary>
        public float Separator
        {
            get => (float)GetValue(SeparatorProperty);
            set => SetValue(SeparatorProperty, value);
        }

        /// <summary>
        /// 获取/设置Content保留的宽度
        /// </summary>
        public float Persist
        {
            get => (float)GetValue(PersistProperty);
            set => SetValue(PersistProperty, value);
        }



        /// <summary>
        /// 获取/设置内容是否显示
        /// </summary>
        public bool ContentEnabled
        {
            get => (bool)GetValue(ContentEnabledProperty);
            set => SetValue(ContentEnabledProperty, value);
        }

        /// <summary>
        /// 添加/移除点击事件
        /// </summary>
        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
    }
}