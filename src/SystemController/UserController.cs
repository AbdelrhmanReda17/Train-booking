using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;
using Train_booking.src.UserClasses;
namespace Train_booking.src.SystemController {
    public class UserController {
        private DataManager Data = new DataManager();
        private TrainsController trainsController = new TrainsController();
        private TripsController tripsController = new TripsController();

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

            Customer ct = new Customer(username, name, password, phone, email, city, age, country);
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
                Console.Write("Please Select one of options: ");
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

        private void CustomerLogin() {
            string username = string.Empty;
            string password = string.Empty;
            if (!TakeInputString(ref username, "Username")) return;
            if (!TakeInputString(ref password, "Password")) return;
            Customer? customer = Data.GetCustomer(username, password);
            if (customer == null) {
                Console.WriteLine("Customer not Found or wrong password!");
                return;
            }
            CustomerInterface(customer);
        }

        private void CustomerInterface(Customer customer) {
            int number;
            do {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Hello " + customer.name);
                Console.WriteLine("1. Book a Trip");
                Console.WriteLine("2. Update Profile");
                Console.WriteLine("0. log out");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Please select which detail you want to change: ");
                int.TryParse(Console.ReadLine(), out number);
                switch (number) {
                    case 1:
                        BookingInterface(customer);
                        break;
                    case 2:
                        ChangeDetails(customer);
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

        private void AdminLogin() {
            string username = string.Empty;
            string password = string.Empty;

            if (!TakeInputString(ref username, "Username")) return;

            if (!TakeInputString(ref password, "Password")) return;

            Admin? admin = Data.GetAdmin(username, password);

            if (admin == null) {
                Console.WriteLine("Admin not Found or wrong password!");
                return;
            }
            AdminInterface(admin);
        }

        private void AdminInterface(Admin admin) {
            int number;
            do {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Hello " + admin.name);
                Console.WriteLine("1. Add a Trip");
                Console.WriteLine("2. Remove a Trip");
                Console.WriteLine("3. Add a Train");
                Console.WriteLine("4. Remove a Train");
                Console.WriteLine("0. log out");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Please select which detail you want to change: ");
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

        private void BookingInterface(Customer customer) {
            int number;
            List<Trip> tripList = Data.GetTrips();
            if (tripList.Count <= 0 || tripList == null) {
                Console.WriteLine("No Available Trips!");
                return;
            }

            // Available trips
            Console.WriteLine("------------------------------------------------");
            int i = 1;
            foreach (Trip trip in tripList) {
                Console.WriteLine($"{i++}) " + trip.ToString());
            }
            Console.WriteLine("0. Cancel Booking");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Please select Trip you want to Book: ");

            while (!int.TryParse(Console.ReadLine(), out number) || number < 0 || number > tripList.Count) {
                Console.Write("Please Select a Valid Trip: ");
            }

            if (number == 0) {
                Console.WriteLine("Booking Canceled!");
                return;
            }

            List<int>? seatList = BookSeats(tripList[number - 1].trip_id);

            if (seatList == null || seatList.Count <= 0) {
                Console.WriteLine("No Seats Added! Canceling Process!");
                return;
            }

            Data.ConfirmBooking(seatList, tripList[number - 1], customer.id);
        }

        private List<int>? BookSeats(int trip_id) {
            int leastSeatID = Data.GetLeastSeatID(trip_id);
            List<int> seatList = new List<int>();
            List<int> availableSeats = Data.GetAvailableSeats(trip_id);
            for (int i = 0; i < availableSeats.Count;i++)
                availableSeats[i] = availableSeats[i] - leastSeatID + 1;



            int number;
            do {
                if (availableSeats == null || availableSeats.Count <= 0) {
                    Console.WriteLine("No Available Seats for this Trip!");
                    break;
                }

                // Avaiable Seats for Trip
                foreach (int seatno in availableSeats) {
                    Console.WriteLine($"| {seatno} |");
                }

                Console.Write("Please Select the seat number you want to book (Enter 0 to finish Adding seats): ");

                int.TryParse(Console.ReadLine(), out number);
                
                if (availableSeats.Contains(number)) {
                    if (seatList.Contains(number + leastSeatID - 1)) {
                        Console.WriteLine("Seat Already Added");
                    } else {
                        Console.WriteLine("Seat Added Successfully!");
                        seatList.Add(number + leastSeatID - 1);
                        availableSeats.Remove(number);
                    }
                } else if (number != 0) {
                    Console.WriteLine("Please pick a valid seat!");
                }
            } while (number != 0);

            if (seatList == null || seatList.Count <= 0) {
                Console.WriteLine("No Seats Added! Canceling Process!");
                return null;
            }

            int temp;
            Console.WriteLine("1) Confirm Booking\n0) Cancel Booking");
            int.TryParse(Console.ReadLine(), out temp);
            while (temp != 0 || temp != 1) {
                if (temp == 0) return null;
                if (temp == 1) break;

                Console.WriteLine("Please Input A valid number!");
                Console.WriteLine("1) Confirm Booking\n0) Cancel Booking");
                int.TryParse(Console.ReadLine(), out temp);
            }

            return seatList;
        }

        private void ChangeDetails(Customer customer) {
            string change = string.Empty;
            int choice;
            do {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine(" 1. Name");
                Console.WriteLine(" 2. Password");
                Console.WriteLine(" 3. Email");
                Console.WriteLine(" 4. Phone number");
                Console.WriteLine(" 5. City");
                Console.WriteLine(" 6. Country");
                Console.WriteLine(" 0. Exit and save");
                Console.WriteLine("------------------------------------------------");
                Console.Write(" Please select which detail you want to change: ");
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
                        Console.WriteLine("Changes Saved Successfully!");
                        break;
                    default:
                        Console.WriteLine("Invalid Input! Please Try Again!");
                        break;
                }
            } while (choice != 0);

            // Update Database
            if (change != string.Empty) {
                Data.UpdateCustomer(customer);
            }
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