using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_booking.src.SystemClasses {
    public class Trip {
        public int trip_id { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
        public int train_id { get; set; }
        public string tripDate { get; set; }
        public double price { get; set; }

        public Trip(string source, string destination, int train_id, double price, string tripDate) {
            this.source = source;
            this.destination = destination;
            this.train_id = train_id;
            this.price = price;
            this.tripDate = tripDate;
        }

        public override string ToString() {
            return $"{source} | {destination} | {price} | {train_id}";
        }
    }
}