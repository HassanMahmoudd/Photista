using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Photista.Model
{
    public class PhotoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public BitmapImage ImagePath { get; set; }
        public string ImagePath1 { get; set; }
        public bool IsFavorites { get; set; }  //Hassan added
        public BitmapImage FavouritesIcon { get; set; }  //Hassan added
    }

}
