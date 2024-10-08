using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    public abstract class Person
    {
        public string Name {  get; set; }
        public string PhoneNumber { get; set; }
        public bool IsCheckedIn { get; set; }
        public DateTime LastCheckIn { get; set; }
        public DateTime LastCheckOut { get; set; }

        public Person(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            IsCheckedIn = false;
        } 

        public string CheckIn()
        {
            if (!IsCheckedIn)
            {
                IsCheckedIn = true;
                LastCheckIn = DateTime.Now;
                return $"{Name} checkede ind {LastCheckIn}";
            }
            else
            {
                return $"{Name} er allerede checket ind";
            }
        }

        public string CheckOut()
        {
            if (IsCheckedIn)
            {
                IsCheckedIn = false;
                LastCheckOut = DateTime.Now;
                return $"{Name} checkede ud {LastCheckOut}";
            }
            else
            {
                return $"{Name} er ikke checket ind på nuværende tidspunkt";
            }
        }

       

    }
}
