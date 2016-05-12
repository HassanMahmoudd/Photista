using Photista.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Photista
{
   
    class JsonHandler
    {
        private const String JSONFILENAME = "MenuItem.json";

        private const String JSONFILENAMEidlist = "PhotoItemData1.json";
        private const String JSONFILENAMEtitlelist = "PhotoItemData2.json";
        private const String JSONFILENAMEDeslist = "PhotoItemData3.json";
        private const String JSONFILENAMECatlist = "PhotoItemData4.json";
        private const String JSONFILENAMEIm1list = "PhotoItemData5.json";
        private const String JSONFILENAMEImlist = "PhotoItemData6.json";
        private const String JSONFILENAMEIsFavlist = "PhotoItemData7.json";
        private const String JSONFILENAMEFavIconlist = "PhotoItemData8.json";

        public static List<MenuItem> MyMenuList;
        public static List<PhotoItem> MyPhotoList = new List<PhotoItem>();
        public static ObservableCollection<PhotoItem> temp = new ObservableCollection<PhotoItem>();
        public static async Task writeJsonAsync()
        {
            MyMenuList = MenuItemFactory.Items;
            var serializer = new DataContractJsonSerializer(typeof(List<MenuItem>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, MyMenuList);
            }
                
        }

        public static async Task deserializeJsonAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<MenuItem>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);
            MyMenuList = (List<MenuItem>)jsonSerializer.ReadObject(myStream);
        }

        

        public static async Task writephotoJsonAsync()
        {
            PhotoItemFactory.getAllPhotoItems(temp);
            foreach(PhotoItem x in temp)
            {
                MyPhotoList.Add(x);
            }

            List<int> idlist = new List<int>();
            List<string> titlelist = new List<string>();
            List<string> Deslist = new List<string>();
            List<string> Catlist = new List<string>();
            List<string> Im1list = new List<string>();
            List<BitmapImage> Imlist = new List<BitmapImage>();
            List<bool> IsFavlist = new List<bool>();
            List<BitmapImage> FavIconlist = new List<BitmapImage>();

            foreach (PhotoItem x in MyPhotoList)
            {
                idlist.Add(x.Id);
                titlelist.Add(x.Title);
                Deslist.Add(x.Description);
                Catlist.Add(x.Category);
                Im1list.Add(x.ImagePath1);
                Imlist.Add(x.ImagePath);
                IsFavlist.Add(x.IsFavorites);
                FavIconlist.Add(x.FavouritesIcon);
            }

            

            var serializer = new DataContractJsonSerializer(typeof(List<int>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEidlist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, idlist);
            }

             serializer = new DataContractJsonSerializer(typeof(List<string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEtitlelist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, titlelist);
            }

             serializer = new DataContractJsonSerializer(typeof(List<string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEDeslist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, Deslist);
            }

             serializer = new DataContractJsonSerializer(typeof(List<string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMECatlist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, Catlist);
            }

             serializer = new DataContractJsonSerializer(typeof(List<string>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEIm1list, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, Im1list);
            }

            serializer = new DataContractJsonSerializer(typeof(List<bool>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEIsFavlist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, IsFavlist);
            }

            //WriteableBitmap WriteableBitmapObject = new WriteableBitmap(Imlist.First().PixelWidth, Imlist.First().PixelHeight);
            

           /* serializer = new DataContractJsonSerializer(typeof(List<BitmapImage>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEFavIconlist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, FavIconlist);
            }

            serializer = new DataContractJsonSerializer(typeof(List<BitmapImage>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAMEImlist, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, Imlist);
            }*/



        }

        public static async Task deserializephotoJsonAsync()
        {
            /*var jsonSerializer = new DataContractJsonSerializer(typeof(List<savetemp>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);
           // MyPhotoList = (List<ListItem>)jsonSerializer.ReadObject(myStream);*/
            
        }










        private async Task<string> ToBase64(byte[] image, uint height, uint width, double dpiX = 96, double dpiY = 96)
        {

            var encoded = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, encoded);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, height, width, dpiX, dpiY, image);
            await encoder.FlushAsync();
            encoded.Seek(0);
            var bytes = new byte[encoded.Size];
            await encoded.AsStream().ReadAsync(bytes, 0, bytes.Length);
            return Convert.ToBase64String(bytes);
        }
        private async Task<string> ToMyBased64String(WriteableBitmap WriteableBitmapObject)
        {
            var bytes = WriteableBitmapObject.PixelBuffer.ToArray();
            return await ToBase64(bytes, (uint)WriteableBitmapObject.PixelWidth, (uint)WriteableBitmapObject.PixelHeight);
        }




    }


  /*  public class savetemp
    {
       // public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImagePath1 { get; set; }
        //public bool IsFavorites { get; set; }  
        public  savetemp( string Title, string Description, string Category, string ImagePath1)
        {
            //this.Id = Id;
            this.Title = Title;
            this.Description = Description;
            this.Category = Category;
            this.ImagePath1 = ImagePath1;
           // this.IsFavorites = IsFavorites;
        }
    }
    */

}
