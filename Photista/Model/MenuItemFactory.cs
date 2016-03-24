using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Photista.Model
{
    public class MenuItemFactory
    {
        public static List<MenuItem> Items;

        public async static void init()
        {
            Items = new List<MenuItem>();
            try {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("MenuItem.json");
                await JsonHandler.deserializeJsonAsync();
                var list = JsonHandler.MyMenuList;
                list.ForEach(p => Items.Add(p));
            }catch(Exception e)
            {
            Items.Add(new MenuItem (){ Icon = "Assets/Me-Icon.png", Category = "Me" });
            Items.Add(new MenuItem (){ Icon = "Assets/Me-Icon.png", Category = "Friends" });
            Items.Add(new MenuItem() { Icon = "Assets/Me-Icon.png", Category = "Favorites" });
            }


        }

        public static ObservableCollection<MenuItem> getMenuItems()
        {
            var ItemsObservable = new ObservableCollection<MenuItem>();
            Items.ForEach(p => ItemsObservable.Add(p));
            return ItemsObservable;
        }

        

        public static void addCategory(ObservableCollection<MenuItem> MenuItems , string name)
        {
            if(Items.Where(p => p.Category == name).ToList().Count != 0)return;
            MenuItem temp = new MenuItem() { Icon = "Assets/Me-Icon.png", Category = name };
            Items.Add(temp);
            MenuItems.Add(temp);
         
        }
    }
}
