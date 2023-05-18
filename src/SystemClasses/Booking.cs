using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_booking.src.SystemClasses
{
    public class Booking
    {
        public int booking_id;
        public int price;
        public DateTime  booking_time;
        public Booking_Class Class;
        public DateTime date;
        public int customer_id;
        public int trip_id;
        public int train_id;
        public Booking(int price , Booking_Class Class , int customer_id , int trip_id  , int train_id){
            this.price = price;
            this.Class = Class;
            this.customer_id = customer_id;
            this.trip_id = trip_id;
            this.train_id = train_id;
            this.date = DateTime.Today;
            this.booking_time = DateTime.Now;
        }
        public override string ToString() {
            return $"{booking_id} | {price} | {Class} | {customer_id} | {trip_id} | {train_id}";
        }
    }
}