using System.Collections;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NiansiThemes.Wpf
{
    [TemplatePart(Name = TOGGLE_BUTTON, Type = typeof(ToggleButton))]
    [TemplatePart(Name = HAMBURGER_MENU_BOX, Type = typeof(ListBox))]
    [TemplatePart(Name = HAMBURGER_MENU_BOX_ITEM, Type = typeof(UIElement))]
    public class HamburgerMenuBox : ContentControl
    {
        private const string TOGGLE_BUTTON = "ToggleButton";
        private const string HAMBURGER_MENU_BOX = "HamburgerMenuBox";
        private const string HAMBURGER_MENU_BOX_ITEM = "HamburgerMenuBoxItem";

        public static readonly DependencyProperty ToggleEnabledProperty =
            DependencyProperty.Register("ToggleEnabled", typeof(bool), typeof(HamburgerMenuBox), new PropertyMetadata(true));

        public static readonly DependencyProperty IsPaneOpenProperty =
            DependencyProperty.Register("IsPaneOpen", typeof(bool), typeof(HamburgerMenuBox), new PropertyMetadata(true));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(HamburgerMenuBox), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(HamburgerMenuBox), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(HamburgerMenuBox), new PropertyMetadata(null));

        public static readonly DependencyProperty ItemContainerStyleProperty =
            DependencyProperty.Register("ItemContainerStyle", typeof(Style), typeof(HamburgerMenuBox), new PropertyMetadata(null));

        public static readonly RoutedEvent HamburgerMenuSelectionChangedEvent =
            EventManager.RegisterRoutedEvent("HamburgerMenuSelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(HamburgerMenuBox));

        public bool ToggleEnabled
        {
            get => (bool)GetValue(ToggleEnabledProperty);
            set => SetValue(ToggleEnabledProperty, value);
        }

        public bool IsPaneOpen
        {
            get => (bool)GetValue(IsPaneOpenProperty);
            set => SetValue(IsPaneOpenProperty, value);
        }

        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public Style ItemContainerStyle
        {
            get => (Style)GetValue(ItemContainerStyleProperty);
            set => SetValue(ItemContainerStyleProperty, value);
        }

        public event RoutedEventHandler HamburgerMenuSelectionChanged
        {
            add => AddHandler(HamburgerMenuSelectionChangedEvent, value);
            remove => RemoveHandler(HamburgerMenuSelectionChangedEvent, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var toggleButton = GetTemplateChild(TOGGLE_BUTTON) as ToggleButton;
            if (toggleButton != null)
                toggleButton.Click += ToggleButton_Click;

            var hamburgerMenuBox = GetTemplateChild(HAMBURGER_MENU_BOX) as ListBox;
            if (hamburgerMenuBox != null)
                hamburgerMenuBox.SelectionChanged += HamburgerMenuBox_SelectionChanged;
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            IsPaneOpen = !IsPaneOpen;
        }

        private void HamburgerMenuBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = e.AddedItems[0];
            RaiseEvent(new RoutedEventArgs(HamburgerMenuSelectionChangedEvent));
        }
    }

    public class HamburgerMenuBoxCollection : FreezableCollection<IconContentControl>
    {
        protected override Freezable CreateInstanceCore()
        {
            return new HamburgerMenuBoxCollection();
        }
    }
}
