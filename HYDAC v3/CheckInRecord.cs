using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYDAC_v3
{
    internal class CheckInRecord
    {
        private List<Person> people = new List<Person>();
        private Queue<Person> recentCheckouts = new Queue<Person>();
        private const int MAX_RECENT_CHECKOUTS = 10;
        private Person currentUser = null;

        private List<Guest> guests = new List<Guest>();

        private List<Employee> employees = new List<Employee>
        {
            new Employee ("Jonathan", "23656298"),
            new Employee ("Daniel", "50505050"),
            new SecurityPersonnel ("Mike", "60606060")
        };

        public CheckInRecord ()
        {
            
        }

        public bool LogIn(string phoneNumber)
        {
            var user = employees.Find(e => e.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                currentUser = user;
                //Console.WriteLine($"Velkommen {user.Name}!");
                return true;
            }

            var guest = people.Find(g => g is Guest && g.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
            if (guest != null)
            {
                currentUser = guest;
                //Console.WriteLine($"Velkommen {guest.Name}!");
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

        public bool RegisterGuest(string name, string company, string phoneNumber)
        {
            if (people.Any(p => p.PhoneNumber.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Dette telefonnummer eksisterer allerede i systemet. Prøv at logge ind.");
                Console.ReadKey();
                return false;
            }

            var guest = new Guest(name, phoneNumber, company);
            people.Add(guest);
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
                if (currentUser is Employee)
                {
                    people.Add(currentUser);
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
                if (currentUser is Employee)
                {
                    people.Remove(currentUser);
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
            foreach (var person in people)
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
    }
}
