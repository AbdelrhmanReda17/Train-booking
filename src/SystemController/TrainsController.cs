using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;

namespace Train_booking.src.SystemController
{
    public class TrainsController
    {
        public DataManager Data = new DataManager();
        public TrainsController(){}
            public void AddTrain(){
                int number;
                Console.WriteLine("Please enter the number of the seats of the Train ( 0 to cancel ) :");
                int.TryParse(Console.ReadLine(), out number);
                while(number <= 0){
                    if(number == 0) {
                        Console.WriteLine("Operation Canceled");
                        return;
                    }
                    Console.WriteLine("Invaild input please enter again: ");
                    int.TryParse(Console.ReadLine(), out number);
                }
                Data.AddTrain(number);
        }
            public void RemoveTrain(){
                List<Train>? lst = Data.GetTrains();
                if (lst != null && lst.Count > 0){
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("ID | Total Number Of Seats");
                    for(int i = 1 ; i <=lst.Count ; i++){
                        string outputString = $" {i} | {lst[i-1].total_seats}";
                        Console.WriteLine(outputString);                    
                    }
                    Console.WriteLine("------------------------------------------------");
                    int number , replace;
                    Console.WriteLine("Please enter the Id of the Train ( 0 to cancel ) :");
                    int.TryParse(Console.ReadLine(), out number);
                    while(number <= 0 || number > lst.Count){
                        if(number == 0) {
                            Console.WriteLine("Operation Canceled");
                            return;
                        }
                        Console.WriteLine("Invaild input please enter again: ");
                        int.TryParse(Console.ReadLine(), out number);
                    }
                    if(!Data.CheckTrain(lst[number-1].train_id)){
                        Console.WriteLine("Please enter anthor Train id to replace it into [booking/trip] ( 0 to cancel ) :");
                        int.TryParse(Console.ReadLine(), out replace);
                        while(replace <= 0 || number > lst.Count || replace == number){
                            if(replace == 0) {
                                Console.WriteLine("Operation Canceled");
                                return;
                            }
                            Console.WriteLine("Invaild input please enter again: ");
                            int.TryParse(Console.ReadLine(), out number);
                        }
                        Data.replaceTrain(lst[number-1].train_id ,lst[replace-1].train_id);
                    }
                    Data.RemoveTrain(lst[number-1].train_id);
                }
                else{
                    Console.WriteLine("No Train to remove");
                    return;
                }
            }
    }
}