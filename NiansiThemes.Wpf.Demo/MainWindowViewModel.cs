using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace NiansiThemes.Wpf.Demo
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ICommand HamburgerMenuBoxItemClick
        {
            get => new RelayCommand(() =>
            {
                
            });
        }

        public ICommand HamburgerMenuBoxClick
        {
            get => new RelayCommand(() =>
            {

            });
        }
    }
}
