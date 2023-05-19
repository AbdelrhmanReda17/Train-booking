using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;

namespace Train_booking.src.SystemController {
    public class TripsController {
        public DataManager Data = new DataManager();

        public void Addtrip() {
            string source = string.Empty;
            string destination = string.Empty;
            decimal price;
            if (!TakeInputString(ref source, "source")) return;
            if (!TakeInputString(ref destination, "destination")) return;
            if (source == destination) {
                Console.WriteLine("Source cannot be equal to destination !");
                Addtrip();
            }
            Console.Write($"Please enter the trip price (Enter 0 to Cancel): ");
            decimal.TryParse(Console.ReadLine(), out price);
            while (price < 0) {
                if (price == 0) {
                    return;
                } else {
                    Console.WriteLine("Invaild input please enter again: ");
                    decimal.TryParse(Console.ReadLine(), out price);
                }
            }
            List<Train>? lst = Data.GetTrains();
            if (lst != null && lst.Count > 0) {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("ID | Total Number Of Seats");
                for (int i = 1; i <= lst.Count; i++) {
                    string outputString = $" {i} | {lst[i - 1].total_seats}";
                    Console.WriteLine(outputString);
                }
                Console.WriteLine("------------------------------------------------");
                int number;
                Console.WriteLine("Please enter the Id of the Train ( 0 to cancel ) :");
                int.TryParse(Console.ReadLine(), out number);
                while (number <= 0 || number > lst.Count) {
                    if (number == 0) {
                        Console.WriteLine("Operation Canceled");
                        return;
                    }
                    Console.WriteLine("Invaild input please enter again: ");
                    int.TryParse(Console.ReadLine(), out number);
                }
                Trip tp = new Trip(source, destination, lst[number - 1].train_id, price);
                Data.AddTrip(tp);
            } else {
                Console.WriteLine("Due to no trains availabe , Trip add operation is canceled ");
                return;
            }
        }

        public void Removetrip() {
            List<Trip>? lst = Data.GetTrips();
            if (lst != null && lst.Count > 0) {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("ID | Source | Destination | Price | Train id");
                for (int i = 1; i <= lst.Count; i++) {
                    string outputString = $" {i} | {lst[i - 1]}";
                    Console.WriteLine(outputString);
                }
                Console.WriteLine("------------------------------------------------");
                int number;
                Console.Write("Please enter the Id of the Trip ( 0 to cancel ):");
                int.TryParse(Console.ReadLine(), out number);
                while (number <= 0 || number > lst.Count) {
                    if (number == 0) {
                        Console.WriteLine("Operation Canceled");
                        return;
                    }
                    Console.Write("Invaild input please enter again: ");
                    int.TryParse(Console.ReadLine(), out number);
                }
                // Data.returnSeats     // Done
                // Data.removebooking   // Done
                Data.RemoveTrip(lst[number - 1].trip_id);
            }
        }
        private bool TakeInputString(ref string str, string strname) {
            string? temp;
            Console.Write($"Please enter the trip {strname} (Enter 0 to Cancel): ");
            temp = Console.ReadLine();
            while (string.IsNullOrEmpty(temp)) {
                Console.WriteLine("Please Enter a valid input!");
                Console.Write($"Please enter the trip {strname} (Enter 0 to Cancel): ");
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