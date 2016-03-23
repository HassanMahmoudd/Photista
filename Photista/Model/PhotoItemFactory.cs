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
        public static List<ListItem> AllLists;
        public static List<PhotoItem> unCategorized;
        public static List<PhotoItem> Me;
        public static List<PhotoItem> Friends;
        public static List<PhotoItem> Favorites;

        public static ListItem temp;

        public static void getPhotoItemsByCategory(string Category, ObservableCollection<PhotoItem> PhotoItems)
        {
            PhotoItems.Clear();
            temp = AllLists.Find(p => p.category == Category);
            if(temp != null)
            temp.list.ForEach(p => PhotoItems.Add(p));           
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
                List<PhotoItem> temp = AllLists[i].list;
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

        public static void deletePhotoItem(ObservableCollection<PhotoItem> PhotoItems, PhotoItem photoItem)
        {
            AllLists.Where(p => p.category == photoItem.Category);
            temp = AllLists.Find(p => p.category == photoItem.Category);
            if (temp != null) temp.list.Remove(photoItem);
            PhotoItems.Remove(photoItem);
        }
           
        public static void addtofavorite(PhotoItem photoItem)
        {
            Favorites.Add(photoItem);
        }
        public static void removefromfavorite(PhotoItem photoItem)
        {
            Favorites.Remove(photoItem);
        }

        public static ObservableCollection<PhotoItem> Items;

        public static void init()
        {           
            AllLists = new List<ListItem>();
            Me = new List<PhotoItem>();
            Friends = new List<PhotoItem>();
            unCategorized = new List<PhotoItem>();
            Favorites = new List<PhotoItem>();
            temp = new ListItem();
            AllLists.Add(new ListItem("Me",Me));
            AllLists.Add(new ListItem("Friends",Friends));
            AllLists.Add(new ListItem("unCategorized", unCategorized));




        }

        public static void updatePhotoItems(ObservableCollection<PhotoItem> PhotoItems, PhotoItem photoItem)
        {
            AllLists.Where(p => p.category == photoItem.Category);
            temp = AllLists.Find(p => p.category == photoItem.Category);
            if (temp != null) temp.list.Add(photoItem);           
            PhotoItems.Add(photoItem);
        }

        public static void updatePhotoItemsAfterEdit(ObservableCollection<PhotoItem> PhotoItems, PhotoItem photoItem, String oldCategory)
        {
            AllLists.Where(p => p.category == photoItem.Category);
            temp = AllLists.Find(p => p.category == photoItem.Category);
            if (temp != null) temp.list.Add(photoItem);
            AllLists.Where(p => p.category == oldCategory);
            temp = AllLists.Find(p => p.category == oldCategory);
            if (temp != null) temp.list.Remove(photoItem);
            //PhotoItems.Add(photoItem);
        }

        public static void addCategory(string name)
        {
            AllLists.Add(new ListItem(name, new List<PhotoItem>()));
        }

        public static ObservableCollection<PhotoItem> getPhotoItems()
        {
            return Items;
        }
    }
}
