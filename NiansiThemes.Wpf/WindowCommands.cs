using System.Windows;
using System.Windows.Controls;

namespace NiansiThemes.Wpf
{
    [TemplatePart(Name = CONTENT_PRESENTER, Type = typeof(UIElement))]
    public class WindowCommands : ItemsControl
    {
        private const string CONTENT_PRESENTER = "ContentPresenter";

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(WindowCommands));

        /// <summary>
        /// 添加/移除点击事件
        /// </summary>
        public event RoutedEventHandler Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }
    }
}
