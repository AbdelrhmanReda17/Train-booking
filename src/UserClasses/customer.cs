namespace Train_booking.src.UserClasses;
public class Customer : User{
        public int id { get; set; }
        public Customer(){

        }
        public Customer(string name , string password , string phone, string email, string city , int age , string country) : base(name ,password,phone , email, city, age, country){
        }
        public override string ToString()
        {
            return $"{name} | {password} | {phone} | {email} | {city} | {age} | {country}";
        }
}
