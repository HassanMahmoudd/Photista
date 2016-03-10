using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photista.Model
{
    public class MenuItemFactory
    {
        public static List<MenuItem> Items;

        public static void init()
        {
            Items = new List<MenuItem>();
            Items.Add(new MenuItem (){ Icon = "Assets/Me-Icon.png", Category = "Me" });
            Items.Add(new MenuItem (){ Icon = "Assets/Friends-Icon.png", Category = "Friends" });
        }

        public static ObservableCollection<MenuItem> getMenuItems()
        {
            var ItemsObservable = new ObservableCollection<MenuItem>();
            Items.ForEach(p => ItemsObservable.Add(p));
            return ItemsObservable;
        }
    }
}
