using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Photista.Model;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Photista
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<PhotoItem> PhotoItems;
        private ObservableCollection<MenuItem> MenuItems;
        public MainPage()
        {
            this.InitializeComponent();
            PhotoItems = new ObservableCollection<PhotoItem>();
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItemFactory.init();
            MenuItems = MenuItemFactory.getMenuItems();
            PhotoItemFactory.init();
            PhotoItemFactory.getAllPhotoItems(PhotoItems);

        }

        

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var MenuItemTemp = (MenuItem)e.ClickedItem;
            PhotoItemFactory.getPhotoItemsByCategory(MenuItemTemp.Category, PhotoItems);
            TitleTextBlock.Text = MenuItemTemp.Category;
        }
    }
}
