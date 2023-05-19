using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_booking.src.SystemClasses {
    public class Booking {
        public int booking_id;
        public int price;
        public string booking_time;
        public string date;
        public int customer_id;
        public int trip_id;
        public int train_id;
        
        public Booking(int price, int customer_id, int trip_id, int train_id, string tripDate, string booking_time) {
            this.price = price;
            this.customer_id = customer_id;
            this.trip_id = trip_id;
            this.train_id = train_id;
            this.date = tripDate;
            this.booking_time = booking_time;
        }

        public override string ToString() {
            return $"{booking_id} | {price} | {customer_id} | {trip_id} | {train_id}";
        }
    }
}