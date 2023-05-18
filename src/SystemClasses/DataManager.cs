using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;

namespace Train_booking.src.SystemClasses {
    public class DataManager {
        public Customer? GetCustomer(string name, string password) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Customer? customer = null;

            string selectCustomerQuery = $"Select * from Customer Where Name = '{name}'";
            using (SqlCommand command = new SqlCommand(selectCustomerQuery, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (name == reader["name"].ToString() && password == reader["password"].ToString()) {
                    customer = new Customer();
                    customer.id = reader.GetInt32(0);
                    customer.name = reader.GetString(1);
                    customer.password = reader.GetString(2);
                    customer.phone = reader.GetString(3);
                    customer.email = reader.GetString(4);
                    customer.city = reader.GetString(5);
                    customer.age = reader.GetInt32(6);
                    customer.country = reader.GetString(7);
                }
            }
            con.Close();

            return customer;
        }

        public Admin? GetAdmin(string name, string password) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Admin? admin = null;

            string selectAdminQuery = $"Select * from Admin Where Name = '{name}'";
            using (SqlCommand command = new SqlCommand(selectAdminQuery, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (name == reader["name"].ToString() && password == reader["password"].ToString()) {
                    admin = new Admin();
                    admin.id = reader.GetInt32(0);
                    admin.name = reader.GetString(1);
                    admin.password = reader.GetString(2);
                    admin.phone = reader.GetString(3);
                    admin.email = reader.GetString(4);
                    admin.position = reader.GetString(5);
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

            string query = "insert into Customer values (@id, @name, @password , @phone , @email , @city , @age , @country)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@id", 55);
                command.Parameters.AddWithValue("@name", customer.name);
                command.Parameters.AddWithValue("@password", customer.password);
                command.Parameters.AddWithValue("@phone", customer.phone);
                command.Parameters.AddWithValue("@email", customer.email);
                command.Parameters.AddWithValue("@city", customer.city);
                command.Parameters.AddWithValue("@age", customer.age);
                command.Parameters.AddWithValue("@country", customer.country);
                int result = command.ExecuteNonQuery();

                // Check Error
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                    return false;
                }
            }
            return true;
        }
    }
}