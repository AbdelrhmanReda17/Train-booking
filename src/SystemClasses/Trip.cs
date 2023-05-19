using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_booking.src.SystemClasses {
    public class Trip {
        public int trip_id;
        public string source;
        public string destination;
        public int train_id;
        public decimal price;

        public Trip(string source, string destination, int train_id, decimal price) {
            this.source = source;
            this.destination = destination;
            this.train_id = train_id;
            this.price = price;
        }

        public override string ToString() {
            return $"{trip_id} | {source} | {destination} | {train_id} | {price}";
        }
    }
}