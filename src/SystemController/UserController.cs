using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;
using Train_booking.src.UserClasses;
namespace Train_booking.src.SystemController {
    public class UserController {
        public DataManager Data = new DataManager();

        public void RegisterInterface() {
            string name = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            string phone = string.Empty;
            string city = string.Empty;
            string country = string.Empty;

            if (!TakeInputString(ref name, "Name")) return;

            if (!TakeInputString(ref password, "Password")) return;

            if (!TakeInputString(ref email, "Email")) return;

            if (!TakeInputString(ref phone, "Phone Number")) return;

            if (!TakeInputString(ref city, "City")) return;

            if (!TakeInputString(ref country, "Country")) return;

            int age;
            Console.WriteLine("Please enter your Age :  ");
            while (!int.TryParse(Console.ReadLine(), out age)) {
                Console.Write("Please enter vaild Age: ");
            }

            Customer ct = new Customer(name, password, phone, email, city, age, country);
            Data.AddCustomer(ct);
        }

        public void LoginInterface() {
            int number;
            do {
                Console.WriteLine("Login as:");
                Console.WriteLine(" 1. Customer");
                Console.WriteLine(" 2. Admin");
                Console.WriteLine(" 0. Go Back");
                Console.Write("Please Select one of options : ");

                int.TryParse(Console.ReadLine(), out number);

                switch (number) {
                    case 1:
                        CustomerInterface();
                        break;
                    case 2:
                        AdminInterface();
                        break;
                    case 0:
                        Console.WriteLine("Login Canceled!");
                        break;
                    default:
                        Console.WriteLine("Please Select a valid input!");
                        break;
                }
            } while (number != 0);
        }

        public void CustomerInterface() {
            string name = string.Empty;
            string password = string.Empty;

            if (!TakeInputString(ref name, "Name")) return;

            if (!TakeInputString(ref password, "Password")) return;

            Customer? customer = Data.GetCustomer(name, password);

            if (customer == null) {
                Console.WriteLine("Customer not Found or wrong password!");
                return;
            }

            Console.WriteLine(customer.ToString());
        }

        public void AdminInterface() {
            string name = string.Empty;
            string password = string.Empty;

            if (!TakeInputString(ref name, "Name")) return;

            if (!TakeInputString(ref password, "Password")) return;

            Admin? admin = Data.GetAdmin(name, password);

            if (admin == null) {
                Console.WriteLine("Admin not Found or wrong password!");
                return;
            }

            Console.WriteLine(admin.ToString());
        }

        private bool TakeInputString(ref string str, string strname) {
            string? temp;
            Console.Write($"Please enter your {strname} (Enter 0 to Cancel): ");
            temp = Console.ReadLine();
            while (string.IsNullOrEmpty(temp)) {
                Console.WriteLine("Please Enter a valid input!");
                Console.Write($"Please enter your {strname} (Enter 0 to Cancel): ");
                temp = Console.ReadLine();
            }

            if (temp == "0") {
                Console.WriteLine("Process Canceled!");
                return false;
            }

            str = temp;
            return true;
        }
    }
}