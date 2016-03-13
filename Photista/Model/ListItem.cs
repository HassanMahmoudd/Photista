using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Photista.Model
{
    public class ListItem
    {
        public string category;
        public List<PhotoItem> list;

        public ListItem()
        {
        }

        public ListItem(string category, List<PhotoItem> list)
        {
            this.category = category;
            this.list = list;
        }
    }
}
