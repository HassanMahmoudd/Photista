using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photista.Model
{
    public class PhotoItemFactory
    {
        public static List<List<PhotoItem>> AllLists;
        public static List<PhotoItem> Me;
        public static List<PhotoItem> Friends;
        public static void getPhotoItemsByCategory(string Category, ObservableCollection<PhotoItem> PhotoItems)
        {
            PhotoItems.Clear();
            if(Category == "Me")
            {
                Me.ForEach(p => PhotoItems.Add(p));
            }
            else if(Category == "Friends")
            {
                Friends.ForEach(p => PhotoItems.Add(p));
            }
            //var AllItems = getPhotoItems();
            //var FilteredItems = PhotoItems.Where(p => p.Category == Category).ToList();
            //PhotoItems.Clear();
            //FilteredItems.ForEach(p => PhotoItems.Add(p));
        }
        public static void getAllPhotoItems(ObservableCollection<PhotoItem> PhotoItems)
        {
            PhotoItems.Clear();
            for (int i = 0; i < AllLists.Count; i++)
            {
                List<PhotoItem> temp = AllLists[i];
                temp.ForEach(p => PhotoItems.Add(p));
            }
            //PhotoItems = Items;
            //var AllItems = getPhotoItems();
            //PhotoItems.Clear();
            //AllItems.ForEach(p => PhotoItems.Add(p));
        }

       /* public static void getCategory(string Title)
        {
            
        }*/
        public static void getPhotoItemByTitle(ObservableCollection<PhotoItem> PhotoItems, string Title)
        {
            PhotoItems.Clear();
            ObservableCollection<PhotoItem> allPhotoItems = new ObservableCollection<PhotoItem>();
            getAllPhotoItems(allPhotoItems);
            if (allPhotoItems != null) { 
            var filteredPhotoItems = allPhotoItems.Where(p => p.Title == Title).ToList();
                
            filteredPhotoItems.ForEach(p => PhotoItems.Add(p));
        }
             

        }


        public static ObservableCollection<PhotoItem> Items;
        public static void init()
        {
            AllLists = new List<List<PhotoItem>>();
            Me = new List<PhotoItem>();
            Friends = new List<PhotoItem>();
            AllLists.Add(Me);
            AllLists.Add(Friends);
            //Items = new ObservableCollection<PhotoItem>();
            //Items.Add(new PhotoItem() { Id = 1, Title = "Hassan 01", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 01.jpg" });
            //Items.Add(new PhotoItem() { Id = 2, Title = "Hassan 02", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 02.jpg" });
            //Items.Add(new PhotoItem() { Id = 3, Title = "Hassan 03", Description = "Photo Test", Category = "Me", ImagePath = "Assets/Hassan 03.jpg" });
            //Items.Add(new PhotoItem() { Id = 4, Title = "Friends 01", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 01.jpg" });
            //Items.Add(new PhotoItem() { Id = 5, Title = "Friends 02", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 02.jpg" });
            //Items.Add(new PhotoItem() { Id = 6, Title = "Friends 03", Description = "Photo Test", Category = "Friends", ImagePath = "Assets/Friends 03.jpg" });


        }
        public static void updatePhotoItems(ObservableCollection<PhotoItem> PhotoItems, PhotoItem photoItem)
        {
            if(photoItem.Category == "Me")
            {
                Me.Add(photoItem);
            }
            else if(photoItem.Category == "Friends")
            {
                Friends.Add(photoItem);
            }
            PhotoItems.Add(photoItem);
        }

        public static ObservableCollection<PhotoItem> getPhotoItems()
        {
            return Items;
        }
    }
}
