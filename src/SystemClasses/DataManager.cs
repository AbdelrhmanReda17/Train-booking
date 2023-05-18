using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;

namespace Train_booking.src.SystemClasses {
    public class DataManager {
        public Customer? GetCustomer(string username, string password) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Customer? customer = null;
            string selectCustomerQuery = $"Select * from Customer Where username = '{username}'";
            using (SqlCommand command = new SqlCommand(selectCustomerQuery, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (username == reader["username"].ToString() && password == reader["password"].ToString()) {
                    customer = new Customer();
                    customer.id = reader.GetInt32(0);
                    customer.name = reader.GetString(1);
                    customer.username = reader.GetString(2);
                    customer.password = reader.GetString(3);
                    customer.phone = reader.GetString(4);
                    customer.email = reader.GetString(5);
                    customer.city = reader.GetString(6);
                    customer.age = reader.GetInt32(7);
                    customer.country = reader.GetString(8);
                }
            }
            con.Close();

            return customer;
        }

        public Admin? GetAdmin(string username, string password) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Admin? admin = null;

            string selectAdminQuery = $"Select * from Admin Where username = '{username}'";
            using (SqlCommand command = new SqlCommand(selectAdminQuery, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                if (username == reader["username"].ToString() && password == reader["password"].ToString()) {
                    admin = new Admin();
                    admin.id = reader.GetInt32(0);
                    admin.name = reader.GetString(1);
                    admin.username = reader.GetString(2);
                    admin.password = reader.GetString(3);
                    admin.phone = reader.GetString(4);
                    admin.email = reader.GetString(5);
                    admin.position = reader.GetString(6);
                    return admin;
                }
            }
            con.Close();
            return admin;
        }
        public bool AddCustomer(Customer customer) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            int ch = 0;
            string query = $"select COUNT(username) from customer where username = '{customer.username}'";
            using (SqlCommand command = new SqlCommand(query , con)){
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                ch = reader.GetInt32(0);
                if(ch == 1){
                    Console.WriteLine("Username already taken");
                    return false;
                }
            }
            query = "insert into Customer(username,name,password, phone_number, email, city, age, country) values (@name, @username,  @password , @phone , @email , @city , @age , @country)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@name", customer.name);
                command.Parameters.AddWithValue("@username", customer.username);
                command.Parameters.AddWithValue("@password", customer.password);
                command.Parameters.AddWithValue("@phone", customer.phone);
                command.Parameters.AddWithValue("@email", customer.email);
                command.Parameters.AddWithValue("@city", customer.city);
                command.Parameters.AddWithValue("@age", customer.age);
                command.Parameters.AddWithValue("@country", customer.country);
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                    return false;
                }
            }
            return true;
        }
    }
}