using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Train_booking.src.SystemClasses
{
    public class Train
    {
        public int train_id;
        public int total_seats;
        public Train(int train_id , int total_seats){
            this.total_seats = total_seats;
            this.train_id = train_id;
        }

        
    }
}