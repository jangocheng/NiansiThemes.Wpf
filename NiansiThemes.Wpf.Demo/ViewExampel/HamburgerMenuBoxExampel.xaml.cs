using NiansiThemes.Wpf.Demo.ViewModel;
using System.Windows.Controls;

namespace NiansiThemes.Wpf.Demo.ViewExampel
{
    /// <summary>
    /// HamburgerMenuBoxExampel.xaml 的交互逻辑
    /// </summary>
    public partial class HamburgerMenuBoxExampel : UserControl
    {
        private HamburgerMenuBoxExampelViewModel _vm;
        public HamburgerMenuBoxExampel()
        {
            InitializeComponent();
            _vm = new HamburgerMenuBoxExampelViewModel();
            DataContext = _vm;
        }
    }
}
