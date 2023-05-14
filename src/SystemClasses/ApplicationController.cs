using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.UserClasses;

namespace  Train_booking.src.SystemClasses
{
    public class ApplicationController
    {
        public void Start()
        {
            Console.WriteLine("APPLICATION START");

            User user = new Customer("John", "Doe", "Male", 30, "johndoe@email.com", "01270953626");

            Console.WriteLine(user.ToString());

            Console.WriteLine("APPLICATION END");
        }
    }
}