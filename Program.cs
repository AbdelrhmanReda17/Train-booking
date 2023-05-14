using System;
using Train_booking.src.SystemClasses;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationController app = new ApplicationController();
            app.Start();
        }
    }
}