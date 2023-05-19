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

        public void UpdateCustomer(Customer customer) {
            string connectionString = "Server=ABDELRHMAN\\SQLEXPRESS; Initial Catalog=Train-Booking; Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string updateCustomerQuery = "UPDATE CUSTOMER SET name = @name, password = @password, email = @email, "
                    + $"phone_number = @phone, country = @country, city = @city WHERE username = {customer.username}";
                using (SqlCommand command = new SqlCommand(updateCustomerQuery, connection)) {
                    command.Parameters.AddWithValue("@name", customer.name);
                    command.Parameters.AddWithValue("@password", customer.password);
                    command.Parameters.AddWithValue("@email", customer.email);
                    command.Parameters.AddWithValue("@phone", customer.phone);
                    command.Parameters.AddWithValue("@country", customer.country);
                    command.Parameters.AddWithValue("@city", customer.city);
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("Error updating data in the database!");
                    } else {
                        Console.WriteLine("Customer details updated successfully.");
                    }
                }
            }
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
            string query = $"Select COUNT(username) from customer where username = '{customer.username}'";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                ch = reader.GetInt32(0);
                if (ch >= 1) {
                    Console.WriteLine("Username already taken");
                    con.Close();
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
                con.Close();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                    return false;
                }
            }
            return true;
        }

        public void AddTrain(int ts) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Train(total_seats) values (@total_seats)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@total_seats", ts);
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                } else {
                    Console.WriteLine("Train added successfully.");
                }
            }
            con.Close();
        }

        public bool CheckTrain(int ts) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Boolean isCorrect = true;
            int bk, tp;
            string query = $"select COUNT(*) from Booking where train_id = '{ts}'";
            using (SqlCommand command = new SqlCommand(query, con)) {
                bk = (int)command.ExecuteScalar();
                if (bk > 0) {
                    isCorrect = false;
                }
            }
            query = $"select COUNT(*) from Trip where train_id = '{ts}'";
            using (SqlCommand command = new SqlCommand(query, con)) {
                tp = (int)command.ExecuteScalar();
                if (tp > 0) {
                    isCorrect = false;
                }
            }
            if (isCorrect)
                return true;
            else
                return false;
        }

        // public Boolean replaceTrain(int ts, int replace) {
        //     string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
        //     SqlConnection con = new SqlConnection(str);
        //     con.Open();
        //     string query = $"Delete from Train where train_id={ts}";
        //     using (SqlCommand command = new SqlCommand(query, con)) {
        //         command.Parameters.AddWithValue("@replaced", replace);
        //         command.Parameters.AddWithValue("@oldid", ts);
        //         int result = command.ExecuteNonQuery();
        //         if (result < 0) {
        //             Console.WriteLine("Error in delete data in Database!");
        //             return false;
        //         }
        //     }
        //     query = $"Update Trip SET train_id=@replaced where train_id = @oldid";
        //     using (SqlCommand command = new SqlCommand(query, con)) {
        //         command.Parameters.AddWithValue("@replaced", replace);
        //         command.Parameters.AddWithValue("@oldid", ts);
        //         int result = command.ExecuteNonQuery();
        //         if (result < 0) {
        //             Console.WriteLine("Error in delete data in Database!");
        //             return false;
        //         }
        //     }
        //     return true;
        // }

        public void RemoveTrain(int ts) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = $"Delete from Train where train_id={ts}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error deleting data from the database!");
                } else {
                    Console.WriteLine("Train deleted successfully.");
                }
            }
            con.Close();
        }

        public List<Train> GetTrains() {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            List<Train> lst = new List<Train>();  // Instantiate the list
            con.Open();
            string query = "Select * from Train";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    int trainId = reader.GetInt32(0);
                    int totalSeats = reader.GetInt32(1);
                    Train train = new Train(totalSeats);
                    train.train_id = trainId;
                    lst.Add(train);
                }
                reader.Close();
            }
            return lst;
        }

        public void AddTrip(Trip tp) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Trip(source,destination,price,train_id) values (@source , @destination, @price , @train_id)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@source", tp.source);
                command.Parameters.AddWithValue("@destination", tp.destination);
                command.Parameters.AddWithValue("@price", tp.price);
                command.Parameters.AddWithValue("@train_id", tp.train_id);
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                } else {
                    Console.WriteLine("Trip added successfully.");
                }
            }
            con.Close();
        }

        public void RemoveTrip(int tp) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            CheckTrip(tp);
            con.Open();
            string query = $"Delete from Trip where trip_id={tp}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error delating data into Database!");
                } else {
                    Console.WriteLine("Trip deleted successfully.");
                }
            }
            con.Close();
        }

        public void CheckTrip(int tp) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();

            // Get the booking ID for the specified trip.
            int? bk = null;
            string query = $"SELECT Booking_id FROM Booking WHERE trip_id={tp}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int bookingId)) {
                    bk = bookingId;
                }
            }


            if (bk.HasValue) {
                // Free all seats for the specified booking ID.
                query = $"UPDATE Seat SET state = 0 WHERE booking_id={bk}";
                using (SqlCommand command = new SqlCommand(query, con)) {
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("No seats to free.");
                    } else {
                        Console.WriteLine("Seats related to this trip has been freed successfully.");
                    }
                }

                // Delete all booking that is related to this trip
                query = $"DELETE FROM Booking WHERE trip_id={tp}";
                using (SqlCommand command = new SqlCommand(query, con)) {
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("No booking to delete.");
                    } else {
                        Console.WriteLine("Booking related to this trip has been deleted successfully.");
                    }
                }
            } else {
                Console.WriteLine("No booking found for the specified trip ID.");
            }
            con.Close();
        }

        public List<Trip> GetTrips() {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            List<Trip> lst = new List<Trip>();  // Instantiate the list
            con.Open();
            string query = "Select * from Trip";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    int trip_id = reader.GetInt32(0);
                    string trip_source = reader.GetString(1);
                    string trip_destination = reader.GetString(2);
                    decimal trip_price = reader.GetDecimal(3);
                    int trip_train = reader.GetInt32(4);
                    Trip tp = new Trip(trip_source, trip_destination, trip_train, trip_price);
                    tp.trip_id = trip_id;
                    lst.Add(tp);
                }
                reader.Close();
            }
            return lst;
        }

        public List<int> GetTripsRelatedToTrain(int train_id) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            List<int> tripIDList = new List<int>();  // Instantiate the list
            con.Open();
            string query = $"Select * from Trip Where train_id = {train_id}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    int trip_id = reader.GetInt32(0);

                    tripIDList.Add(trip_id);
                }
                reader.Close();
            }
            return tripIDList;
        }

        public void RemoveSeats(int train_id) {
            string str = "Server = ABDELRHMAN\\SQLEXPRESS; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();

            string query = $"DELETE FROM Seat WHERE Train_train_id={train_id}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("No Seats to delete.");
                } else {
                    Console.WriteLine("Seats related to this trip has been deleted successfully.");
                }
            }
            con.Close();
        }
    
        public List<int> GetAvailableSeats(int trip_id) {
            throw new NotImplementedException();
        }
    }
}