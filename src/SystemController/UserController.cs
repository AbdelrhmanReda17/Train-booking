using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;
using Train_booking.src.UserClasses;
namespace Train_booking.src.SystemController {
    public class UserController {
        public DataManager Data = new DataManager();
        public TrainsController trainsController = new TrainsController();
        public TripsController tripsController = new TripsController();
        public void RegisterInterface() {
            string name = string.Empty;
            string username = string.Empty;
            string password = string.Empty;
            string email = string.Empty;
            string phone = string.Empty;
            string city = string.Empty;
            string country = string.Empty;

            if (!TakeInputString(ref name, "Name")) return;

            if (!TakeInputString(ref username, "Username")) return;

            if (!TakeInputString(ref password, "Password")) return;

            if (!TakeInputString(ref email, "Email")) return;

            if (!TakeInputString(ref phone, "Phone Number")) return;

            if (!TakeInputString(ref city, "City")) return;

            if (!TakeInputString(ref country, "Country")) return;

            int age;
            Console.Write("Please enter your Age : ");
            while (!int.TryParse(Console.ReadLine(), out age)) {
                Console.Write("Please enter vaild Age: ");
            }

            Customer ct = new Customer(username,name, password, phone, email, city, age, country);
            Data.AddCustomer(ct);
        }

        public void LoginInterface() {
            int number;
            do {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Login as:");
                Console.WriteLine("1. Customer");
                Console.WriteLine("2. Admin");
                Console.WriteLine("0. Go Back");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Please Select one of options : ");
                int.TryParse(Console.ReadLine(), out number);

                switch (number) {
                    case 1:
                        CustomerLogin();
                        return;
                    case 2:
                        AdminLogin();
                        return;
                    case 0:
                        Console.WriteLine("Login Canceled!");
                        break;
                    default:
                        Console.WriteLine("Please Select a valid input!");
                        break;
                }
            } while (number != 0);
        }

        public void CustomerLogin() {
            string username = string.Empty;
            string password = string.Empty;
            if (!TakeInputString(ref username, "Username")) return;
            if (!TakeInputString(ref password, "Password")) return;
            Customer? customer = Data.GetCustomer(username, password);
            if (customer == null) {
                Console.WriteLine("Customer not Found or wrong password!");
                return;
            }
            CustomerInterface(ref customer);
        }
        public void CustomerInterface(ref Customer customer){
            int number ;
            do {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine(" 1. Book a Trip");
            Console.WriteLine(" 2. Change your details");
            Console.WriteLine(" 0. log out");
            Console.WriteLine("------------------------------------------------");
            Console.Write(" Please select which detail you want to change : ");
            int.TryParse(Console.ReadLine(), out number);
            switch (number) {
                    case 1:
                       // Booking();
                        break;
                    case 2:
                        ChangeDetails(ref customer);
                        break;
                    case 0:
                        Console.WriteLine("Log out Successfully");
                        break;
                    default:
                        Console.WriteLine("Please Select a valid input!");
                        break;
                }
            } while (number != 0);
        }
        public void AdminInterface(ref Admin Admin){
            int number ;
            do {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Hello " + Admin.name);
            Console.WriteLine(" 1. Apdd a Trip");
            Console.WriteLine(" 2. Remove a Trip");
            Console.WriteLine(" 3. Add a Train");
            Console.WriteLine(" 4. Remove a Train");
            //Console.WriteLine(" 5. Edit a Customer");
            //Console.WriteLine(" 5. Remove a Customer");
            Console.WriteLine(" 0. log out");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Please select which detail you want to change : ");
            int.TryParse(Console.ReadLine(), out number);
            switch (number) {
                    case 1:
                        tripsController.Addtrip();
                        break;
                    case 2:
                        tripsController.Removetrip();
                        break;
                    case 3:
                        trainsController.AddTrain();
                        break;
                    case 4:
                        trainsController.RemoveTrain();
                        break;
                    case 0:
                        Console.WriteLine("Log out Successfully");
                        break;
                    default:
                        Console.WriteLine("Please Select a valid input!");
                        break;
                }
            } while (number != 0);
        }
        public void ChangeDetails(ref Customer customer){
            string change = string.Empty;
            int choice;
            do{
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine(" 1. Name");
                Console.WriteLine(" 2. Password");
                Console.WriteLine(" 3. Email");
                Console.WriteLine(" 4. Phone number");
                Console.WriteLine(" 5. City");
                Console.WriteLine(" 6. Country");
                Console.WriteLine(" 0. Exit and save");
                Console.WriteLine("------------------------------------------------");
                Console.Write(" Please select which detail you want to change : ");
                int.TryParse(Console.ReadLine(), out choice);
                switch (choice) {
                    case 1:
                        if (!TakeInputString(ref change, "Name")) break;
                        customer.name = change;
                        break;
                    case 2:
                        if (!TakeInputString(ref change, "Password")) break;
                        customer.password = change;
                        break;
                    case 3:
                        if (!TakeInputString(ref change, "Email")) break;
                        customer.email = change;
                        break;
                    case 4:
                        if (!TakeInputString(ref change, "phonenumber")) break;
                        customer.phone = change;
                        break;
                    case 5:
                        if (!TakeInputString(ref change, "city")) break;
                        customer.city = change;
                        break;
                    case 6:
                        if (!TakeInputString(ref change, "country")) break;
                        customer.country = change;
                        break;                      
                    case 0:
                        Data.UpdateCustomer(customer);
                        break;
                    default:
                        break;
                }
            }while(choice != 0);
        }
        public void AdminLogin() {
            string username = string.Empty;
            string password = string.Empty;

            if (!TakeInputString(ref username, "Username")) return;

            if (!TakeInputString(ref password, "Password")) return;

            Admin? admin = Data.GetAdmin(username, password);

            if (admin == null) {
                Console.WriteLine("Admin not Found or wrong password!");
                return;
            }
           AdminInterface(ref admin);
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