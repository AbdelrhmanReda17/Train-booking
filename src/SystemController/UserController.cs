using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Train_booking.src.SystemClasses;
using Train_booking.src.UserClasses;
namespace Train_booking.src.SystemController
{
    public class UserController 
    {
        public DataManager Data = new DataManager();
        public UserController()
        {
        }
        public void RegisterInterface(){
            Console.WriteLine("Please enter your name : ");
            string? name = Console.ReadLine();
            while(string.IsNullOrEmpty(name)){
                Console.WriteLine("Please Enter vaild Name :  ");
                name = Console.ReadLine();
            }
            Console.WriteLine("Please enter your password :  ");
            string? password = Console.ReadLine();
            while(string.IsNullOrEmpty(password)){
                Console.WriteLine("Please Enter vaild password :  ");
                password = Console.ReadLine();
            }
            Console.WriteLine("Please enter your email :  ");
            string? email = Console.ReadLine();
            while(string.IsNullOrEmpty(email)){
                Console.WriteLine("Please Enter vaild Email :  ");
                email = Console.ReadLine();
            }
            Console.WriteLine("Please enter your phone number :  ");
            string? phone = Console.ReadLine();
            while(string.IsNullOrEmpty(phone)){
                Console.WriteLine("Please Enter vaild phone number :  ");
                phone = Console.ReadLine();
            }
            Console.WriteLine("Please enter your city :  ");
            string? city = Console.ReadLine();
            while(string.IsNullOrEmpty(city)){
                Console.WriteLine("Please Enter vaild city :  ");
                city = Console.ReadLine();
            }
            Console.WriteLine("Please enter your country :  ");
            string? country = Console.ReadLine();
            while(string.IsNullOrEmpty(country)){
                Console.WriteLine("Please Enter vaild country :  ");
                country = Console.ReadLine();
            }
            Console.WriteLine("Please enter your Age :  ");
            int age;
            string? input = Console.ReadLine();
            while (!int.TryParse(input, out age)){
                Console.WriteLine("Please enter vaild Age:");
                input = Console.ReadLine();
            }
            Customer ct = new Customer(name , password , phone , email,city,age , country);
            Data.addCustomer(ct);
        }
        public void LoginInterface()
        {
            Console.WriteLine("Login as:");
            Console.WriteLine(" 1. Customer");
            Console.WriteLine(" 2. Admin");
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("Please Select one of options : ");
            int number;
            string? input = Console.ReadLine();
            while (!int.TryParse(input, out number)){
                Console.WriteLine("Please enter a valid choice:");
                input = Console.ReadLine();
            }
            if (number == 1){
                CustomerInterface();
            }
            else if (number == 2){
                AdminInterface();
            }
            else
            {
                return;
            }
        }
        public void CustomerInterface(){

            Console.WriteLine("Please enter your name : ");
            string? name = Console.ReadLine();
            while(string.IsNullOrEmpty(name)){
                Console.WriteLine("Please Enter vaild Name :  ");
                name = Console.ReadLine();
            }
            Console.WriteLine("Please enter your Password :  ");
            string? password = Console.ReadLine();
            while(string.IsNullOrEmpty(password)){
                Console.WriteLine("Please Enter vaild Password :  ");
                password = Console.ReadLine();
            }
            Customer? ct = Data.getCustomer(name , password);
            if(ct != null){
                    Console.WriteLine(ct.ToString());
            }else{
                    Console.WriteLine("Customer not Found or wrong password !");
            }
        }
        public void AdminInterface(){
            Console.WriteLine("Please enter your name : ");
            string? name = Console.ReadLine();
            while(string.IsNullOrEmpty(name)){
                Console.WriteLine("Please Enter Invaild Name :  ");
                name = Console.ReadLine();
            }
            Console.WriteLine("Please enter your Password :  ");
            string? password = Console.ReadLine();
            while(string.IsNullOrEmpty(password)){
                Console.WriteLine("Please Enter Invaild Password :  ");
                password = Console.ReadLine();
            }
            Admin? ct = Data.getAdmin(name , password );
            if(ct != null){
                    Console.WriteLine(ct.ToString());
            }else{
                    Console.WriteLine("Admin not Found or wrong password !");
            }
        }

    }
}