using System;
namespace Train_booking.src.UserClasses;

public class Admin : User {
    public int id;
    public string? position;
    public Admin() { }
    public Admin(string username,int id, string position, string name, string password, string phone, string email) : base(username , name, password, phone, email, "0", 0, "0") {
        this.position = position;
        this.id = id;
    }

    public override string ToString() {
        return $"{name} | {password} | {phone} | {email} | {position}";
    }
}
