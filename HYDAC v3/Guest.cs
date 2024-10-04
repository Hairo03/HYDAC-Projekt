using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class Guest : Person
    {
        public string AssignedMeetingRoom { get; set; }
        string Company { get; set; }
        string PhoneNumber { get; set; }
        public Guest(string name, string phoneNumber, string company) : base(name, phoneNumber)
        {
            AssignedMeetingRoom = null;
            Company = company;
            PhoneNumber = phoneNumber;
        }
    }
}
