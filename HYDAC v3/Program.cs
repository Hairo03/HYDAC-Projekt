namespace HYDAC_v3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CheckInRecord checkInRecord = new CheckInRecord();

            Menu mainMenu = new Menu("Velkommen til HYDAC!");

            mainMenu.AddMenuItem("Login");
            mainMenu.AddMenuItem("Registrer");
            mainMenu.AddMenuItem("Exit");

            Menu employeeMenu = new Menu("");

            employeeMenu.AddMenuItem("Indcheckede personer på nuværende tidspunkt");
            employeeMenu.AddMenuItem("Seneste udcheckningner");
            employeeMenu.AddMenuItem("Gå tilbage");

            bool isLoggedIn = false;
            bool active = true;

            while (true)
            {
                // Check if user is logged in
                if (!isLoggedIn)
                {
                    // Main menu
                    mainMenu.Show();
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        // Login
                        case "1":
                            Console.Clear();
                            Console.Write("Indtast telefonnummer: ");
                            string phoneNumber = Console.ReadLine();
                            Console.Clear();
                            isLoggedIn = checkInRecord.LogIn(phoneNumber);
                            break;

                        // Register
                        case "2":
                            Console.Clear();
                            Console.Write("Indtast navn: ");
                            string guestName = Console.ReadLine();
                            Console.Write("Indtast telefonnummer: ");
                            string guestPhoneNumber = Console.ReadLine();
                            Console.Write("Indtast firmanavn: ");
                            string guestCompany = Console.ReadLine();
                            if (checkInRecord.RegisterGuest(guestName, guestCompany, guestPhoneNumber))
                            {
                                isLoggedIn = checkInRecord.LogIn(guestPhoneNumber);
                            }
                            break;

                        // Exit
                        case "3":
                            return;

                        default:
                            Console.WriteLine("Forkert input. Prøv venligst igen");
                            break;
                    }
                    continue;
                }

                // Logged in menu
                Menu loggedInMenu = new Menu($"Velkommen {checkInRecord.GetUserName()}!");

                loggedInMenu.AddMenuItem("Check Ind");
                loggedInMenu.AddMenuItem("Check Ud");
                if (checkInRecord.IsEmployee())
                {
                    loggedInMenu.AddMenuItem("Sikkerhedspersonale Menu");
                }
                loggedInMenu.AddMenuItem("Log ud");

                while (active)
                {
                    loggedInMenu.Show();

                    string menuChoice = Console.ReadLine();


                    // Check if current user is employee
                    switch (checkInRecord.IsEmployee())
                    {

                        // Employee menu
                        case true:

                            switch (menuChoice)
                            {
                                // Check current user in
                                case "1":
                                    loggedInMenu.MenuTitle = checkInRecord.CheckInCurrentUser();
                                    break;

                                // Check current user out
                                case "2":
                                    loggedInMenu.MenuTitle = checkInRecord.CheckOutCurrentUser();
                                    break;

                                case "3":
                                    while (active)
                                    {
                                        employeeMenu.Show();

                                        string employeeMenuChoice = Console.ReadLine();

                                        switch (employeeMenuChoice)
                                        {
                                            case "1":
                                                Console.Clear();
                                                checkInRecord.ListCheckedInPeople();
                                                Console.ReadKey();
                                                break;

                                            case "2":
                                                Console.Clear();
                                                checkInRecord.ListRecentCheckouts();
                                                Console.ReadKey();
                                                break;

                                            case "3":
                                                Console.Clear();
                                                active = false;
                                                break;

                                            default:
                                                Console.Clear();
                                                Console.WriteLine("Forkert input. Prøv venligst igen");
                                                Console.ReadKey();
                                                break;
                                        }
                                    }
                                    active = true;
                                    break;

                                // Log out
                                case "4":
                                    checkInRecord.LogOut();
                                    isLoggedIn = false;
                                    active = false;
                                    break;

                                default:
                                    Console.WriteLine("Forkert input. Prøv venligst igen");
                                    break;

                            }
                            break;


                        // Guest menu
                        case false:
                            switch (menuChoice)
                            {

                                // Check current user in
                                case "1":
                                    loggedInMenu.MenuTitle = checkInRecord.CheckInCurrentUser();
                                    break;

                                // Check current user out
                                case "2":
                                    loggedInMenu.MenuTitle = checkInRecord.CheckOutCurrentUser();
                                    break;

                                // Log out
                                case "3":
                                    checkInRecord.LogOut();
                                    isLoggedIn = false;
                                    active = false;
                                    break;

                                default:
                                    Console.WriteLine("Forkert input. Prøv venligst igen");
                                    break;

                            }
                            break;
                    }
                }
                active = true;
            }
        }
    }
}
