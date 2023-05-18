using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;
using Train_booking.src.SystemController;

namespace  Train_booking.src.SystemClasses
{
    public class ApplicationController
    {
        public UserController usercontroller = new UserController();
        public void Start()
        {
            while(true)
            {
                Console.WriteLine("Welcome to Train-Booking ");
                Console.WriteLine(" 1. Login ");
                Console.WriteLine(" 2. Register ");
                Console.WriteLine(" 0. Exit");
                Console.WriteLine("Please Select one of options : ");
                int number;
                string input = Console.ReadLine() ?? "";
                if(!string.IsNullOrEmpty(input) && int.TryParse(input, out number)){
                    if(number == 1){
                        usercontroller.LoginInterface();
                    }else if(number == 2){
                        usercontroller.RegisterInterface();
                    }else if (number ==0){
                        Console.WriteLine("Thanks");
                        break;
                    }
                }else{
                    Console.WriteLine("Please Select one of options : ");
                }
            }
            // DataManager data = new DataManager();
            // Customer ct = data.loadCustomer("Hazem 1Amr" , "password123");
            // if(ct != null)
            //     Console.WriteLine(ct);
            // else
            //     Console.WriteLine("Customer not found");
        }
    }
}