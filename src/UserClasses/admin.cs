namespace src.UserClasses;
public class Admin : User{
    public Admin(string FirstName, string LastName, string Gender, int Age, string Email , string Phone) : base(FirstName, LastName, Gender, Age, Email, Phone){
         Console.WriteLine("ADMIN");
    }

}
