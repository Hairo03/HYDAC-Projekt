using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class Employee : Person
    {

        public Employee(string name, string phoneNumber) : base(name, phoneNumber)
        {
        }
        

        //TODO: Add room class
        //public void AssignMeetingRoom(Guest guest, Room room);

    }
}
