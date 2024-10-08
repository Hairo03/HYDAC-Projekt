using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class CheckInRecord
    {
        private string filepath = "guestlogins.txt";

        private List<Person> checkedInPeople = new List<Person>();
        private Queue<Person> recentCheckouts = new Queue<Person>();
        
        // The amount of recent checkouts shown in the checkout history
        private const int MAX_RECENT_CHECKOUTS = 10;
        
        private Person currentUser = null;

        private List<Guest> guests = new List<Guest>();

        // List of employees needs to be changed manually to add new employees
        private List<Employee> employees = new List<Employee>
        {
            new Employee ("Jonathan", "23656298"),
            new Employee ("Daniel", "50505050"),
            new SecurityPersonnel ("Mike", "60606060")
        };

        public CheckInRecord ()
        {
            guests = LoadGuestList();
        }

        public bool LogIn(string phoneNumber)
        {
            var employee = employees.Find(e => e.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            if (employee != null)
            {
                currentUser = employee;
                return true;
            }

            var guest = guests.Find(g => g is Guest && g.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            if (guest != null)
            {
                currentUser = guest;
                return true;
            }
            Console.WriteLine("Forkert indtastning eller gæsten blev ikke fundet i systemet. Prøv venligst igen");
            return false;
        }

        public void LogOut()
        {
            if (currentUser != null)
            {
                currentUser = null;
            }
        }

        public bool RegisterGuest(string name, string company, string phoneNumber, bool safetyFolderReceived)
        {
            if (guests.Any(p => p.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Dette telefonnummer eksisterer allerede i systemet. Prøv at logge ind.");
                Console.ReadKey();
                return false;
            }

            var guest = new Guest(name, phoneNumber, company, safetyFolderReceived);
            guests.Add(guest);
            SaveToList(guest);
            Console.Clear();
            Console.WriteLine($"Gæst registrering fuldført: {name}, {company}");
            Thread.Sleep(2000);
            Console.Clear();
            return true;
        }

        public string CheckInCurrentUser()
        {
            if (currentUser != null)
            {
                if (currentUser is Person)
                {
                    checkedInPeople.Add(currentUser);
                }
                return currentUser.CheckIn();
            }
            else
            {
                return "Der er ikke en bruger logget ind på nuværende tidspunkt";
            }
        }

        public string CheckOutCurrentUser()
        {
            string result;

            if (currentUser != null)
            {
                if (currentUser is Guest)
                {
                    checkedInPeople.Remove(currentUser);
                    currentUser.AssignedRoom = null;
                }
                result = currentUser.CheckOut();
                if (!currentUser.IsCheckedIn)
                {
                    recentCheckouts.Enqueue(currentUser);
                    if (recentCheckouts.Count > MAX_RECENT_CHECKOUTS)
                    {
                        recentCheckouts.Dequeue();
                    }
                }
                return result;
            }
            return null;
        }

        public void ListCheckedInPeople()
        {
            if (!(currentUser is SecurityPersonnel))
            {
                Console.WriteLine("Kun sikkerhedspersonalet kan bruge dette menupunkt");
                return;
            }

            Console.WriteLine("Indcheckede personer på nuværende tidspunkt:\n");
            foreach (var person in checkedInPeople)
            {
                string role;

                if (person is Guest)
                {
                    role = "Guest";
                }
                else
                {
                    role = "Employee";
                }

                Console.WriteLine($"{role} - {person.Name} - Checkede ind: {person.LastCheckIn}");
            }
        }

        public void ListRecentCheckouts()
        {
            if (!(currentUser is SecurityPersonnel))
            {
                Console.WriteLine("Kun sikkerhedspersonalet kan bruge dette menupunkt");
                return;
            }

            Console.WriteLine("Seneste check ud: \n");
            foreach (var person in recentCheckouts.Reverse())
            {
                string role;

                if (person is Guest)
                {
                    role = "Guest";
                }
                else if (person is SecurityPersonnel)
                {
                    role = "Security";
                }
                else
                {
                    role = "Employee";
                }

                    Console.WriteLine($"{role} - {person.Name} - Checked out at: {person.LastCheckOut}");
  

            }
        }

        public bool IsEmployee()
        {
            if (currentUser is SecurityPersonnel || currentUser is Employee)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetUserName()
        {
            return currentUser.Name;
        }

        public List<Guest> LoadGuestList()
        {
            StreamReader reader = new StreamReader(filepath);

            int i = 0;

            List<Guest> persons = [];
            string[] array = reader.ReadToEnd().Split("\r\n");

            foreach (var item in array)
            {
                if (item.Length == 0)
                {
                    break;
                }
                string[] line = item.Split(':');

                Console.WriteLine(item);

                string name = line[0];
                string phoneNumber = line[1];
                string company = line[2];
                bool safetyFolder = bool.Parse(line[3]);

                Guest person = new Guest(name, phoneNumber, company, safetyFolder);
                persons.Add(person);
                
                i++;
            }
            reader.Close();
            return persons;
        }

        public void SaveToList(Person person)
        {
            StreamWriter writer = new StreamWriter(filepath);
            writer.WriteLine(person.ToString());
            writer.Close();
        }
    }
}
