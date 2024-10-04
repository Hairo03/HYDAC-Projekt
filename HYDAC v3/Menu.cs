using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class Menu
    {
        private string menuTitle;
        private MenuItem[] menuItems = new MenuItem[10];
        private int itemCount;


        public string MenuTitle
        {
            get { return menuTitle; }
            set { menuTitle = value; }
        }

        public Menu(string menuTitle)
        {
            MenuTitle = menuTitle;
        }

        public void AddMenuItem(string title)
        {
            MenuItem item = new MenuItem(1 + itemCount + ". " + title);
            menuItems[itemCount] = item;
            itemCount++;
        }

        public void Show()
        {
            Console.Clear();

            if (menuTitle != "")
            {
                Console.WriteLine(menuTitle);
            }

            foreach (MenuItem item in menuItems)
            {
                if (itemCount > menuItems.Length || item == null)
                {
                    break;
                }
                Console.WriteLine(item.Title);
            }
            Console.Write("Vælg venligst et menupunkt: ");
        }
    }
}
