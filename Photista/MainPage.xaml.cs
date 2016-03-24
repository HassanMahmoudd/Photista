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
using Windows.UI;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Photista
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PhotoItem photoitem;
        private PhotoItemControl photoitemcontrol;
        private Boolean selected;
        private ObservableCollection<PhotoItem> PhotoItems;
        private ObservableCollection<PhotoItem> tempItems;
        private ObservableCollection<MenuItem> MenuItems;
        private List<String> Suggestions;
        private ObservableCollection<ListItem> MenuItemsWithoutFavourites;  //Hassan added
        private MenuItem menuItemTemp;
        private string Category;
        private bool isFullViewPageActivated;
        private String splitViewPaneLength;  //Hassan added NEW
       
        public MainPage()
        {
            this.InitializeComponent();
            selected = false;
            splitViewPaneLength = "200";  //Hassan added NEW
            photoitem = new PhotoItem();
            photoitemcontrol = new PhotoItemControl();
            PhotoItems = new ObservableCollection<PhotoItem>();
            MenuItems = new ObservableCollection<MenuItem>();
            tempItems = new ObservableCollection<PhotoItem>();
            MenuItemsWithoutFavourites = new ObservableCollection<ListItem>();  //Hassan added
            menuItemTemp = new MenuItem();
            MenuItemFactory.init();
            MenuItems = MenuItemFactory.getMenuItems();
            PhotoItemFactory.init();
            PhotoItemFactory.getAllPhotoItems(PhotoItems);
            MenuItemsWithoutFavourites = PhotoItemFactory.getAllLists();  //Hassan added
            BackButton.Visibility = Visibility.Collapsed;
            EditStackPanel.Visibility = Visibility.Collapsed;
            EditPicButton.Visibility = Visibility.Collapsed;
            Category = "unCategorized";
            GridImage.Visibility = Visibility.Collapsed;
            WaterMarkTextBlock.Visibility = Visibility.Collapsed;
            DeleteCategoryButton.Visibility = Visibility.Collapsed;  //Hassan added NEW NEW



            // testing code
            Uri uri = new Uri("ms-appx:///Assets/Friends 01.JPG");
            BitmapImage i = new BitmapImage(uri);

            PhotoItem photoItem  = new PhotoItem() { Id = 1, Title = title + "1", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 02.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem1 = new PhotoItem() { Id = 2, Title = title + "2", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 03.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem2 = new PhotoItem() { Id = 3, Title = title + "3", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 04.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem3 = new PhotoItem() { Id = 4, Title = title + "4", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 05.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem4 = new PhotoItem() { Id = 5, Title = title + "5", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 06.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem5 = new PhotoItem() { Id = 6, Title = title + "6", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Friends 07.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem6 = new PhotoItem() { Id = 7, Title = title + "7", Description = "Test Photo", Category = "Friends", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Hassan 01.jpg");
            i = new BitmapImage(uri);

            PhotoItem photoItem7 = new PhotoItem() { Id = 8, Title = title1 + "1", Description = "Test Photo", Category = "Me", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Hassan 02.jpg");
            i = new BitmapImage(uri);

            PhotoItem photoItem8 = new PhotoItem() { Id = 9, Title = title1 + "2", Description = "Test Photo", Category = "Me", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Hassan 03.jpg");
            i = new BitmapImage(uri);

            PhotoItem photoItem9 = new PhotoItem() { Id = 10, Title = title1 + "3", Description = "Test Photo", Category = "Me", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Hassan 04.JPG");
            i = new BitmapImage(uri);

            PhotoItem photoItem10 = new PhotoItem() { Id = 11, Title = title1 + "4", Description = "Test Photo", Category = "Me", ImagePath = i };

            uri = new Uri("ms-appx:///Assets/Hassan 05.jpg");
            i = new BitmapImage(uri);

            PhotoItem photoItem11 = new PhotoItem() { Id = 12, Title = title1 + "5", Description = "Test Photo", Category = "Me", ImagePath = i };

            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem1);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem2);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem3);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem4);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem5);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem6);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem7);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem8);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem9);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem10);
            PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem11);

            // End

        }


        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
       
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
                        
            

        }


        public async void saveMenuItems()
        {
            await JsonHandler.writeJsonAsync();
            
        }

        public async void savePhotoItems()
        {
            await JsonHandler.writephotoJsonAsync();

        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(isFullViewPageActivated)
            {
                isFullViewPageActivated = false;
                GridImage.Source = null;
                GridImage.Visibility = Visibility.Collapsed;
                NewsItemGrid.Visibility = Visibility.Visible;
                AddPicButton.Visibility = Visibility.Visible;
                deletePicButton.Visibility = Visibility.Collapsed;
                EditStackPanel.Visibility = Visibility.Collapsed;
                EditPicButton.Visibility = Visibility.Collapsed;  //Hassan added
                flagEditPicButton = false;
            }
            var MenuItemTemp = (MenuItem)e.ClickedItem;
            if(MenuItemTemp.Category == "Favorites")  //Hassan added NEW
                AddPicButton.Visibility = Visibility.Collapsed;  //Hassan added NEW
            else  //Hassan added NEW
                AddPicButton.Visibility = Visibility.Visible;  //Hassan added NEW
            PhotoItemFactory.getPhotoItemsByCategory(MenuItemTemp.Category, PhotoItems);
            TitleTextBlock.Text = MenuItemTemp.Category;
            BackButton.Visibility = Visibility.Visible;
            Category = MenuItemTemp.Category;
            TitleTextBlock.Text = Category;
            menuItemTemp = MenuItemTemp;
            if (menuItemTemp.Category == "Me" || menuItemTemp.Category == "Friends" || menuItemTemp.Category == "Favorites")  //Hassan added NEW NEW
            {
                DeleteCategoryButton.Visibility = Visibility.Collapsed;  //Hassan added NEW NEW
            }
            else
                DeleteCategoryButton.Visibility = Visibility.Visible;  //Hassan added NEW NEW
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            goBack();
            
        }

        private void goBack()
        {
            if(isFullViewPageActivated)
            {
               
                
                if(Category.Equals("unCategorized"))
                {
                    PhotoItemFactory.getAllPhotoItems(PhotoItems);  //Hassan added
                    TitleTextBlock.Text = "All Photos";
                    BackButton.Visibility = Visibility.Collapsed;
                }
                    
                else
                {
                    PhotoItemFactory.getPhotoItemsByCategory(menuItemTemp.Category, PhotoItems);  //Hassan added
                    TitleTextBlock.Text = Category;
                    if (menuItemTemp.Category == "Me" || menuItemTemp.Category == "Friends" || menuItemTemp.Category == "Favorites")  //Hassan added NEW NEW
                    {
                        DeleteCategoryButton.Visibility = Visibility.Collapsed;  //Hassan added NEW NEW
                    }
                    else
                        DeleteCategoryButton.Visibility = Visibility.Visible;  //Hassan added NEW NEW              
                }
                    
               
                isFullViewPageActivated = false;
                GridImage.Source = null;
                GridImage.Visibility = Visibility.Collapsed;
                NewsItemGrid.Visibility = Visibility.Visible;
                AddPicButton.Visibility = Visibility.Visible;
                deletePicButton.Visibility = Visibility.Collapsed;
                EditStackPanel.Visibility = Visibility.Collapsed;
                EditPicButton.Visibility = Visibility.Collapsed;
                flagEditPicButton = false;
            }
            else
            {
                PhotoItemFactory.getAllPhotoItems(PhotoItems);
                TitleTextBlock.Text = "All Photos";
                BackButton.Visibility = Visibility.Collapsed;
                MenuItemsListView.SelectedItem = null;
                Category = "unCategorized";
                SearchAutoSuggestBox.Text = "";
                AddPicButton.Visibility = Visibility.Visible;  //Hassan added NEW
                DeleteCategoryButton.Visibility = Visibility.Collapsed;  //Hassan added NEW NEW
            }           
            
        }

        //testing code
        int ID = 6;
        string title = "Friends ";
        string title1 = "Hassan ";
        //end

        

        private async void NewsItemGrid_Drop(object sender, DragEventArgs e)
        {


           // await JasonHandler.writeJsonAsync();

            

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
                        photoitem = new PhotoItem() { Id = ID, Title = title + ID.ToString() , Description = "Test Photo", Category = Category, ImagePath = bitmapImage};  //Hassan modified NEW
                        ID++;
                        PhotoItemFactory.updatePhotoItems(PhotoItems, photoitem);  //Hassan modified NEW
                        if (photoitem != null)  //Hassan added NEW
                        {
                            changeView();  //Hassan added NEW
                            EditStackPanel.Visibility = Visibility.Visible;  //Hassan added NEW
                            TitleTextBox.Text = "";  //Hassan added NEW
                            DescriptionTextBox.Text = "";  //Hassan added NEW
                            CategoryListBox.SelectedItem = null;  //Hassan added NEW
                        }
                    }
                }
                WaterMarkTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void NewsItemGrid_DragOver(object sender, DragEventArgs e)
        {
            if (menuItemTemp.Category == "Favorites")  //Hassan added NEW
            {
                e.DragUIOverride.Caption = "Can't directly add to favourites";  //Hassan added NEW
            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.Copy;

                e.DragUIOverride.Caption = "Drop to add image";
                e.DragUIOverride.IsCaptionVisible = true;
                e.DragUIOverride.IsContentVisible = true;
                e.DragUIOverride.IsGlyphVisible = true;
            }
            
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
            if (isFullViewPageActivated)
            {
                isFullViewPageActivated = false;
                GridImage.Source = null;
                GridImage.Visibility = Visibility.Collapsed;
                NewsItemGrid.Visibility = Visibility.Visible;
                AddPicButton.Visibility = Visibility.Visible;
                deletePicButton.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(sender.Text)) {return;}

            PhotoItemFactory.getAllPhotoItems(tempItems);
            PhotoItemFactory.setAllPhotoItems(PhotoItems, tempItems.Where(p => p.Title.StartsWith(sender.Text)).ToList());

            //PhotoItemFactory.getPhotoItemByTitle(PhotoItems,sender.Text);
            Debug.WriteLine(sender.Text);
            if (PhotoItems == null )
            {
                PhotoItemFactory.getAllPhotoItems(PhotoItems);
                return;
            }



            if( PhotoItems.Count == 0)  TitleTextBlock.Text = "Title Not Found";
            else TitleTextBlock.Text = sender.Text;
            
            BackButton.Visibility = Visibility.Visible;
            MenuItemsListView.SelectedItem = null;

        }


        private async void AddPicButton_Click(object sender, RoutedEventArgs e)
        {
            await PicPicker.choosePicture(PhotoItems, Category);
            photoitem = PicPicker.photoItem;
            if (photoitem != null)
            {
                changeView();
                EditStackPanel.Visibility = Visibility.Visible;  //Hassan added NEW
                TitleTextBox.Text = "";  //Hassan added NEW
                DescriptionTextBox.Text = "";  //Hassan added NEW
                CategoryListBox.SelectedItem = null;  //Hassan added NEW
            }
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void deletePicButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected == false)deletePicButton.Flyout.Hide();
        }

        object x;

        private void PhotoItemControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            selected = true;
            photoitemcontrol = (PhotoItemControl)sender;
            x = sender;
            photoitem = photoitemcontrol.PhotoItem;
            deletePicButton.Visibility = Visibility.Visible;
            changeView();

        }

        private void changeView()
        {
            GridImage.Source = photoitem.ImagePath;
            NewsItemGrid.Visibility = Visibility.Collapsed;
            GridImage.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            TitleTextBlock.Text = photoitem.Title;
            AddPicButton.Visibility = Visibility.Collapsed;
            EditPicButton.Visibility = Visibility.Visible;
            DeleteCategoryButton.Visibility = Visibility.Collapsed;  //Hassan added NEW NEW
            isFullViewPageActivated = true;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            if (selected)
            {
                if (Category.Equals("Favorites"))
                {
                    if (PhotoItemFactory.isfavorite(photoitem))
                    {
                        PhotoItemFactory.removefromfavorite(photoitem);
                        photoitem.FavouritesIcon = null;
                        photoitem.IsFavorites = false;  //Hassan added NEW
                    }
                }
                else 
                {
                    if (PhotoItemFactory.isfavorite(photoitem)) PhotoItemFactory.removefromfavorite(photoitem);
                    PhotoItemFactory.deletePhotoItem(PhotoItems, photoitem);
                }
                

                selected = !selected;
            }
            deletePicButton.Flyout.Hide();
            deletePicButton.Visibility = Visibility.Collapsed;
            goBack();

        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            deletePicButton.Flyout.Hide();
            //deletePicButton.Visibility = Visibility.Collapsed;  //Hassan removed
            //selected = !selected;  //Hassan removed
           
        }

        bool flagEditPicButton = false;

        private void EditPicButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (!flagEditPicButton)
            {
                EditStackPanel.Visibility = Visibility.Visible;
                TitleTextBox.Text = photoitem.Title;
                DescriptionTextBox.Text = photoitem.Description;
                flagEditPicButton = !flagEditPicButton;
                

                ListItem temp = PhotoItemFactory.AllLists.Find(p => p.category == photoitem.Category);  //Hassan added
                CategoryListBox.SelectedItem = temp;  //Hassan added
                //temp = MenuItems.Select(p => p.Category == photoitem.Category);               

            }
            else
            {
                EditStackPanel.Visibility = Visibility.Collapsed;
                flagEditPicButton = !flagEditPicButton;
            }
                
        }
        
        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            if(TitleTextBox.Text == "" || DescriptionTextBox.Text == "" || CategoryListBox.SelectedItem == null) //Hassan added NEW
            {
                SaveButtonTextBlock.Text = "Please fill all the blanks";
            }
            else
            {
                photoitem.Title = TitleTextBox.Text;
                photoitem.Description = DescriptionTextBox.Text;
                String oldCategory = photoitem.Category;

                photoitem.Category = ((ListItem)CategoryListBox.SelectedItem).category;  //Hassan added
                PhotoItemFactory.updatePhotoItemsAfterEdit(PhotoItems, photoitem, oldCategory);
                TitleTextBlock.Text = TitleTextBox.Text;
                SaveButtonTextBlock.Text = "Changes saved successfully";  //Hassan added NEW
            }
            
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string flyoutCategoryName = Mytextbox.Text;
            if (!string.IsNullOrEmpty(flyoutCategoryName))
            {
                MenuItemFactory.addCategory(MenuItems, flyoutCategoryName);
                PhotoItemFactory.addCategory(flyoutCategoryName);
                PhotoItemFactory.getPhotoItemsByCategory(flyoutCategoryName, PhotoItems);
                TitleTextBlock.Text = flyoutCategoryName;
                BackButton.Visibility = Visibility.Visible;
                Category = flyoutCategoryName;
               
                foreach(var p in MenuItems)  //Hassan added NEW
                {

                    if (p.Category.Length > 10)
                        splitViewPaneLength = ((p.Category.Length)*15).ToString();
                }
                AddCategoryButton.Flyout.Hide();
            }
            
        }

        private void FavouritesButton_Click(object sender, RoutedEventArgs e)  //Hassan added
        {

            if (photoitem.IsFavorites == false)  //Hassan added
            {
                PhotoItemFactory.addtofavorite(photoitem);  //Hassan added
                photoitem.IsFavorites = true;  //Hassan added
                Uri uri = new Uri("ms-appx:///Assets/Star2.png");
                BitmapImage i = new BitmapImage(uri);  //Hassan added
                photoitem.FavouritesIcon = i;  //Hassan added
                FavouritesButtonTextBlock.Text = "Added successfully to Favourites";  //Hassan added NEW
            }
            else
                FavouritesButtonTextBlock.Text = "Already added to Favourites!";  //Hassan added NEW

        }

        private void okSaveButton_Click(object sender, RoutedEventArgs e)  //Hassan added NEW
        {
            SaveButtonFlyout.Hide();
        }

        private void okFavouritesButton_Click(object sender, RoutedEventArgs e)  //Hassan added NEW
        {
            FavouritesButtonFlyout.Hide();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
             saveMenuItems();
             savePhotoItems();
        }
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void yesDeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            MenuItemFactory.Items.Remove(menuItemTemp);
            MenuItems.Remove(menuItemTemp);
            var temp = PhotoItemFactory.AllLists.Find(p => p.category == menuItemTemp.Category);
            PhotoItemFactory.AllLists.Remove(temp);
            goBack();
            DeleteCategoryButtonFlyout.Hide();  //Hassan added NEW NEW
        }

        private void noDeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteCategoryButtonFlyout.Hide();  //Hassan added NEW NEW
        }
    }
}
