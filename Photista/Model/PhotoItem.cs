using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photista.Model
{
    public class PhotoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImagePath { get; set; }
    }

    public class PhotoItemFactory
    {
        public static void getPhotoItemsByCategory(string Category, ObservableCollection<PhotoItem> PhotoItems)
        {
            var AllItems = getPhotoItems();
            var FilteredItems = AllItems.Where(p => p.Category == Category).ToList();
            PhotoItems.Clear();
            FilteredItems.ForEach(p => PhotoItems.Add(p));
        }

        public static List<PhotoItem> getPhotoItems()
        {
            var Items = new List<PhotoItem>();
            Items.Add(new PhotoItem() { Id = 1, Title = "Hassan 01", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 01.jpg" });
            Items.Add(new PhotoItem() { Id = 2, Title = "Hassan 02", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 02.jpg" });
            Items.Add(new PhotoItem() { Id = 3, Title = "Hassan 03", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 03.jpg" });
            Items.Add(new PhotoItem() { Id = 4, Title = "Friends 01", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 01.jpg" });
            Items.Add(new PhotoItem() { Id = 5, Title = "Friends 02", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 02.jpg" });
            Items.Add(new PhotoItem() { Id = 6, Title = "Friends 03", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 03.jpg" });
            return Items;
        }
    }

}
