using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class MenuItem
    {
        private string itemTitle;

        public string Title { get; set; }

        public MenuItem(string title)
        {
            this.Title = title;
        }
    }
}
