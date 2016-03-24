using Photista.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Photista
{
    class JsonHandler
    {
        private const String JSONFILENAME = "data.json";
        public static List<MenuItem> MyMenuList;
        public static List<ListItem> MyPhotoList;
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
            MyPhotoList = MenuItemFactory.Items;
            var serializer = new DataContractJsonSerializer(typeof(List<ListItem>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, MyPhotoList);
            }

        }

        public static async Task deserializephotoJsonAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<ListItem>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);
            MyPhotoList = (List<ListItem>)jsonSerializer.ReadObject(myStream);
        }
    }
}
