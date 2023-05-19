namespace Train_booking.src.UserClasses;
public abstract class User {
    public string? name { get; set; }
    public string? username { get; set; }
    public int age { get; set; }
    public string? email { get; set; }
    public string? phone { get; set; }
    public string? password { get; set; }
    public string? city { get; set; }
    public string? country { get; set; }

    public User() { }
    public User(string username, string name, string password, string phone, string email, string city, int age, string country) {
        this.username = username;
        this.name = name;
        this.country = country;
        this.age = age;
        this.email = email;
        this.phone = phone;
        this.password = password;
        this.city = city;
    }
}
