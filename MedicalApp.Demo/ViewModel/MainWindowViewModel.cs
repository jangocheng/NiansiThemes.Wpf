using MedicalApp.Demo.DataModel;
using System.Collections.Generic;

namespace MedicalApp.Demo.ViewModel
{
    public sealed class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            InitHamburgerMenu();
        }

        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void InitHamburgerMenu()
        {
            HamburgerMenuList = HamburgerMenuDataSource.Instance.InitDataSource();
        }

        private IEnumerable<HamburgerMenu> _hamburgerMenuList;

        public IEnumerable<HamburgerMenu> HamburgerMenuList
        {
            get => _hamburgerMenuList;
            set => _hamburgerMenuList = value;
        }

    }
}
