using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class Guest : Person
    {
        private string assignedRoom;

        public string AssignedRoom { get { return assignedRoom; } set { assignedRoom = value; } }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public bool SafetyFolderReceived { get; set; }
        public Guest(string name, string phoneNumber, string company, bool safetyFolderReceived) : base(name, phoneNumber)
        {
            AssignedRoom = null;
            Company = company;
            PhoneNumber = phoneNumber;
            SafetyFolderReceived = safetyFolderReceived;
        }
        public override string ToString()
        {
            return $"{Name}:{PhoneNumber}:{Company}:{SafetyFolderReceived}";
        }
    }
}
