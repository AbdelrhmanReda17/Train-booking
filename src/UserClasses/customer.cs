namespace Train_booking.src.UserClasses;
public class Customer : User {
    public int id { get; set; }
    public Customer() { }
    public Customer(string username, string name, string password, string phone, string email, string city, int age, string country) : base(username, name, password, phone, email, city, age, country) { }

    public override string ToString() {
        return $"{name} | {username} | {password} | {phone} | {email} | {city} | {age} | {country}";
    }
}
