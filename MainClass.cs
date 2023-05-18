using System;
using Train_booking.src.SystemController;

namespace Train_booking {
    public class MainClass {
        public static void Main(string[] args) {
            ApplicationController app = new ApplicationController();
            app.Start();
        }
    }
}