using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;

namespace Train_booking.src.SystemController {
    public class TrainsController {
        public DataManager Data = new DataManager();

        public void AddTrain() {
            int number;
            Console.WriteLine("Please enter the number of the seats of the Train ( 0 to cancel ) :");
            int.TryParse(Console.ReadLine(), out number);
            while (number <= 0) {
                if (number == 0) {
                    Console.WriteLine("Operation Canceled");
                    return;
                }
                Console.WriteLine("Invaild input please enter again: ");
                int.TryParse(Console.ReadLine(), out number);
            }
            //Data.AddSeats()
            Data.AddTrain(number);
        }

        public void RemoveTrain() {
            List<Train>? trainList = Data.GetTrains();
            if (trainList != null && trainList.Count > 0) {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("ID | Total Number Of Seats");
                for (int i = 1; i <= trainList.Count; i++) {
                    string outputString = $" {i} | {trainList[i - 1].total_seats}";
                    Console.WriteLine(outputString);
                }
                Console.WriteLine("------------------------------------------------");
                int number;
                Console.Write("Please enter the Id of the Train (0 to cancel):");
                while (!int.TryParse(Console.ReadLine(), out number) && (number <= 0 || number > trainList.Count)) {
                    if (number == 0) {
                        Console.WriteLine("Operation Canceled");
                        return;
                    }
                    Console.Write("Invaild input please enter again: ");
                }

                // Select all trips related to this train and remove them
                List<int> tripIDList = Data.GetTripsRelatedToTrain(trainList[number - 1].train_id);
                foreach (int tripID in tripIDList) {
                    // Free Seats / RemoveBooking / RemoveTrip
                    Data.RemoveTrip(tripID);
                }

                Data.RemoveSeats(trainList[number - 1].train_id);
                Data.RemoveTrain(trainList[number - 1].train_id);
            } else {
                Console.WriteLine("No Train to remove");
                return;
            }
        }
    }
}