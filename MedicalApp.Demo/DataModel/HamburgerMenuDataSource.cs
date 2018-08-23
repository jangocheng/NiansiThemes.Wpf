using NiansiCommons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalApp.Demo.DataModel
{
    public abstract class HamburgerMenuDataCommon
    {
        private readonly IEnumerable<HamburgerMenu> _hamburgerMenus = default;

        public HamburgerMenuDataCommon()
        {
            var appPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            var configPath = appPath + "configs\\";
            var menu = configPath + "menu.json";
            var menuItem = configPath + "menuItem.json";
            var menuGeometry = configPath + "menuGeometry.json";
            var menuItemGeometry = configPath + "menuItemGeometry.json";

            var hamburgerMenu = JsonHelper.JsonReader<List<HamburgerMenu>>(menu).Where(i => i.IsActive);
            var hamburgerMenuItem = JsonHelper.JsonReader<List<HamburgerMenuItem>>(menuItem).Where(i => i.IsActive);
            var hamburgerMenuGeometry = JsonHelper.JsonReader<List<HamburgerMenuGeometry>>(menuGeometry);
            var hamburgerMenuItemGeometry = JsonHelper.JsonReader<List<HamburgerMenuItemGeometry>>(menuItemGeometry);

            foreach (var item in hamburgerMenuItem)
            {
                var itemGeometry = hamburgerMenuItemGeometry.Single(i => i.FatherId == item.Id);
                if (itemGeometry != null) item.GeometryData = itemGeometry.GeometryData;
            }

            foreach (var item in hamburgerMenu)
            {
                var itemGeometry = hamburgerMenuGeometry.Single(i => i.FatherId == item.Id);
                if (itemGeometry != null) item.GeometryData = itemGeometry.GeometryData;

                var itemItem = hamburgerMenuItem.Where(i => i.FatherId == item.Id);
                if (itemItem != null) item.HamburgerMenuItems = itemItem;
            }

            _hamburgerMenus = hamburgerMenu;
        }

        public IEnumerable<HamburgerMenu> InitDataSource()
        {
            return _hamburgerMenus;
        }
    }

    public class HamburgerMenuDataSource : HamburgerMenuDataCommon
    {
        private HamburgerMenuDataSource()
        {

        }

        public static HamburgerMenuDataSource Instance
        {
            get => _hamburgerMenuDataSource;
        }
        private static readonly HamburgerMenuDataSource _hamburgerMenuDataSource = new HamburgerMenuDataSource();
    }

    public class HamburgerMenu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GeometryData { get; set; }
        public string Assemblie { get; set; }
        public string EntityName { get; set; }
        public IEnumerable<HamburgerMenuItem> HamburgerMenuItems { get; set; }
        public bool IsActive { get; set; }
    }

    public class HamburgerMenuGeometry
    {
        public string Id { get; set; }
        public string FatherId { get; set; }
        public string GeometryData { get; set; }
    }

    public class HamburgerMenuItem
    {
        public string Id { get; set; }
        public string FatherId { get; set; }
        public string Name { get; set; }
        public string GeometryData { get; set; }
        public string Assemblie { get; set; }
        public string EntityName { get; set; }
        public bool IsActive { get; set; }
    }

    public class HamburgerMenuItemGeometry
    {
        public string Id { get; set; }
        public string FatherId { get; set; }
        public string GeometryData { get; set; }
    }
}
