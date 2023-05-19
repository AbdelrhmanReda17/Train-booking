using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;

namespace Train_booking.src.SystemController {
    public class ApplicationController {
        private UserController usercontroller = new UserController();

        public void Start() {
            int number;
            do {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Welcome to Train-Booking ");
                Console.WriteLine(" 1. Login ");
                Console.WriteLine(" 2. Register ");
                Console.WriteLine(" 0. Exit");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Please Select one of options: ");
                int.TryParse(Console.ReadLine(), out number);
                switch (number) {
                    case 1:
                        usercontroller.LoginInterface();
                        break;
                    case 2:
                        usercontroller.RegisterInterface();
                        break;
                    case 0:
                        Console.WriteLine("Closing Program! Thanks for using!");
                        break;
                    default:
                        Console.WriteLine("Please Select a valid input!");
                        break;
                }
            } while (number != 0);
        }

    }
}