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
        private List<String> MyList; //List which will have he data
        public async Task writeJsonAsync()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<String>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(JSONFILENAME, CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, MyList);
            }
                
        }

        public async Task deserializeJsonAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<String>));
            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);
            List<String> result = (List<String>)jsonSerializer.ReadObject(myStream);
        }
    }
}
