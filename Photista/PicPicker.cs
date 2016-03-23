using Photista.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace Photista
{
    class PicPicker
    {
        public static async void choosePicture(ObservableCollection<PhotoItem> PhotoItems, string Category)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation =
            Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
           
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            
            if (file != null)
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(await file.OpenAsync(FileAccessMode.Read));
                PhotoItem photoItem = new PhotoItem() { Id = 10, Title = file.Name, Description = "Test Photo", Category = Category, ImagePath = bitmapImage };
                PhotoItemFactory.updatePhotoItems(PhotoItems, photoItem);

            }
        }
    }
}
