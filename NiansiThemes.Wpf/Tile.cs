using System.Windows;
using System.Windows.Controls;

namespace NiansiThemes.Wpf
{
    [TemplatePart(Name = ICON, Type = typeof(System.Windows.Shapes.Path))]
    [TemplatePart(Name = TITLE, Type = typeof(UIElement))]
    public class Tile : ContentControl
    {
        private const string ICON = "Icon";
        private const string TITLE = "Title";

        public static readonly DependencyProperty IconGeometryDataProperty =
            DependencyProperty.Register("IconGeometryData", typeof(string), typeof(Tile), new PropertyMetadata(null));

        /// <summary>
        /// 获取/设置显示Icon的SVG格式的数据
        /// </summary>
        public string IconGeometryData
        {
            get => (string)GetValue(IconGeometryDataProperty);
            set => SetValue(IconGeometryDataProperty, value);
        }
    }
}
