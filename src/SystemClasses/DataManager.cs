using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;
namespace Train_booking.src.SystemClasses
{
    public class DataManager
    {        
        public Customer getCustomer(string name , string password){
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Customer c = new Customer();
            using (SqlCommand command = new SqlCommand("Select * from Customer", con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    c.name = reader.GetValue(1).ToString();
                    c.password = reader.GetValue(2).ToString();
                    if(c.name == name && c.password == password){
                        c.id = int.Parse(reader.GetValue(0).ToString());
                        c.phone = reader.GetValue(3).ToString();
                        c.email = reader.GetValue(4).ToString();
                        c.city = reader.GetValue(5).ToString();
                        c.age = int.Parse(reader.GetValue(6).ToString());
                        c.country = reader.GetValue(7).ToString();
                        return c;
                    }
                }
            }
            return null;
        }
        public Admin getAdmin(string name , string password){
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Admin c = new Admin();
            using (SqlCommand command = new SqlCommand("Select * from Admin", con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    c.name = reader.GetValue(1).ToString();
                    c.password = reader.GetValue(2).ToString();
                    if(c.name == name && c.password == password){
                        c.id = int.Parse(reader.GetValue(0).ToString());
                        c.position = reader.GetValue(5).ToString();
                        c.phone = reader.GetValue(3).ToString();
                        c.email = reader.GetValue(4).ToString();
                        return c;
                    }
                }
            }
            return null;
        }

        public Boolean addCustomer(Customer customer){
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Customer values (@name, @password , @phone , @email , @city , @age , @country)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@name", customer.name);
                command.Parameters.AddWithValue("@password", customer.password);
                command.Parameters.AddWithValue("@phone", customer.phone);
                command.Parameters.AddWithValue("@email", customer.email);
                command.Parameters.AddWithValue("@city", customer.city);
                command.Parameters.AddWithValue("@age",customer.age);
                command.Parameters.AddWithValue("@country",customer.country);
                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0)
                    Console.WriteLine("Error inserting data into Database!");
            }
            return true;
        }
    }
}