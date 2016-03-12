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
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Photista
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<PhotoItem> PhotoItems;
        private ObservableCollection<PhotoItem> tempItems;
        private ObservableCollection<MenuItem> MenuItems;
        private List<String> Suggestions;
        public MainPage()
        {
            this.InitializeComponent();
            PhotoItems = new ObservableCollection<PhotoItem>();
            MenuItems = new ObservableCollection<MenuItem>();
            tempItems = new ObservableCollection<PhotoItem>();
            MenuItemFactory.init();
            MenuItems = MenuItemFactory.getMenuItems();
            PhotoItemFactory.init();
            PhotoItemFactory.getAllPhotoItems(PhotoItems);
            BackButton.Visibility = Visibility.Collapsed;

            PhotoItem photoItem = new PhotoItem() { Id = 1, Title = title + "1", Description = "Test Photo", Category = "Me", ImagePath1 = @"Assets\Friends 01.JPG" };
            PhotoItem photoItem1 = new PhotoItem() { Id = 2, Title = title + "2", Description = "Test Photo", Category = "Me", ImagePath1 = @"Assets\Friends 02.JPG" };
            PhotoItem photoItem2 = new PhotoItem() { Id = 3, Title = title + "3", Description = "Test Photo", Category = "Me", ImagePath1 = @"Assets\Friends 03.JPG" };
            PhotoItem photoItem3 = new PhotoItem() { Id = 4, Title = title + "4", Description = "Test Photo", Category = "Friends", ImagePath1 = @"Assets\Hassan 01.JPG" };
            PhotoItem photoItem4 = new PhotoItem() { Id = 5, Title = title + "5", Description = "Test Photo", Category = "Friends", ImagePath1 = @"Assets\Hassan 02.JPG" };

            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem1);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem2);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem3);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem4);



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
            BackButton.Visibility = Visibility.Visible;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            goBack();
            
        }

        private void goBack()
        {           
            PhotoItemFactory.getAllPhotoItems(PhotoItems);
            TitleTextBlock.Text = "All Photos";
            BackButton.Visibility = Visibility.Collapsed;
            MenuItemsListView.SelectedItem = null;
            SearchAutoSuggestBox.Text = "";
        }

        int ID = 3;
        string title = "Jemy";

        private async void NewsItemGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();

                if (items.Any())
                {
                    var storageFile = items[0] as StorageFile;
                    var contentType = storageFile.ContentType;
                    //string sourcePath = @"C:\Users\Geeek\Desktop\Photista\Photista\Assets";
                    StorageFolder folder = ApplicationData.Current.LocalFolder;
                    //var folder1 = await StorageFolder.GetFolderFromPathAsync(@"C:\Users\Geeek\Desktop\Photista\Photista\Assets");
                    var folder2 = KnownFolders.PicturesLibrary;
                    //PathTextBox.Text = folder.Path;

                    if (contentType == "image/png" || contentType == "image/jpeg" || contentType == "image/jpg")
                    {
                        StorageFile newFile = await storageFile.CopyAsync(folder2, storageFile.Name, NameCollisionOption.GenerateUniqueName);
                        var bitmapImage = new BitmapImage();
                        bitmapImage.SetSource(await storageFile.OpenAsync(FileAccessMode.Read));
                        PhotoItem photoItem = new PhotoItem() { Id = ID, Title = title + ID.ToString() , Description = "Test Photo", Category = "Me", ImagePath = bitmapImage , ImagePath1 = @"C:\Users\mahmoud\Pictures\mah.jpg" };
                        ID++;
                        PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem);
                    }
                }
                WaterMarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void NewsItemGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;

            e.DragUIOverride.Caption = "Drop to add image";
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;
            e.DragUIOverride.IsGlyphVisible = true;
        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (string.IsNullOrEmpty(sender.Text)) { return; }
            PhotoItemFactory.getAllPhotoItems(tempItems);
            Suggestions = tempItems.Where(p => p.Title.StartsWith(sender.Text)).Select(p => p.Title).ToList();
            SearchAutoSuggestBox.ItemsSource = Suggestions; 
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

            if (string.IsNullOrEmpty(sender.Text)) {return;}
            PhotoItemFactory.getPhotoItemByTitle(PhotoItems,sender.Text);
            if (PhotoItems == null)
            {
                PhotoItemFactory.getAllPhotoItems(PhotoItems);
                return;
            }

            TitleTextBlock.Text = sender.Text;         
            BackButton.Visibility = Visibility.Visible;
            MenuItemsListView.SelectedItem = null;

        }
    }
}
