using System;
using src.UserClasses;

namespace src.SystemClasses
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