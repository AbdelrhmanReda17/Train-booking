using System;
using System.Data.SqlClient;
using Train_booking.src.UserClasses;

namespace Train_booking.src.SystemClasses {
    public class DataManager {
        public Customer? GetCustomer(string username, string password) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Customer? customer = null;
            try {
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
            } catch { }
            return customer;
        }

        public void UpdateCustomer(Customer customer) {
            string connectionString = "Server=DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog=Train-Booking; Integrated Security=true;";
            using (SqlConnection connection = new SqlConnection(connectionString)) {
                connection.Open();
                string updateCustomerQuery = "UPDATE CUSTOMER SET name = @name, password = @password, email = @email, "
                    + $"phone_number = @phone, country = @country, city = @city WHERE username = '{customer.username}'";
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
                connection.Close();
            }
        }

        public Admin? GetAdmin(string username, string password) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            Admin? admin = null;
            try {
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
                        reader.Close();
                        return admin;
                    }
                }
                con.Close();
            } catch { }
            return admin;
        }

        public bool AddCustomer(Customer customer) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Train(total_seats) values (@total_seats)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@total_seats", ts);
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                }
            }

            int trainID;
            query = "select max(train_id) from Train";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                trainID = reader.GetInt32(0);
                reader.Close();
            }
            query = "insert into Seat(state, Train_train_id) values(@state, @trainID)";
            for (int i = 1; i <= ts; i++) {
                using (SqlCommand command = new SqlCommand(query, con)) {
                    command.Parameters.AddWithValue("@state", 0);
                    command.Parameters.AddWithValue("@trainID", trainID);
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("Error inserting data into Database!");
                    }
                }
            }
            Console.WriteLine("Train added successfully.");
            con.Close();
        }

        public void RemoveTrain(int ts) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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
            con.Close();
            return lst;
        }

        public void AddTrip(Trip tp) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Trip(source, destination, price, trip_date, train_id) values (@source , @destination, @price, @trip_date,@train_id)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@source", tp.source);
                command.Parameters.AddWithValue("@destination", tp.destination);
                command.Parameters.AddWithValue("@price", tp.price);
                command.Parameters.AddWithValue("@trip_date", tp.tripDate);
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
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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

        private void CheckTrip(int tp) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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
                query = $"UPDATE Seat SET state = 0, booking_id = NULL WHERE booking_id in (SELECT Booking_id FROM Booking WHERE trip_id={tp})";
                using (SqlCommand command = new SqlCommand(query, con)) {
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("No seats to free.");
                    } else {
                        Console.WriteLine("Seats related to this trip have been freed successfully.");
                    }
                }

                // Delete all booking that is related to this trip
                query = $"DELETE FROM Booking WHERE trip_id={tp}";
                using (SqlCommand command = new SqlCommand(query, con)) {
                    int result = command.ExecuteNonQuery();
                    if (result < 0) {
                        Console.WriteLine("No booking to delete.");
                    } else {
                        Console.WriteLine("Bookings related to this trip have been deleted successfully.");
                    }
                }
            } else {
                Console.WriteLine("No booking found for the specified trip ID.");
            }
            con.Close();
        }

        public List<Trip> GetTrips() {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            List<Trip> lst = new List<Trip>();  // Instantiate the list
            con.Open();
            string query = "Select * from Trip";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    int trip_id = reader.GetInt32(0);
                    double trip_price = reader.GetDouble(1);
                    string date = reader.GetString(2);
                    string trip_source = reader.GetString(3);
                    string trip_destination = reader.GetString(4);
                    int trip_train = reader.GetInt32(5);
                    Trip tp = new Trip(trip_source, trip_destination, trip_train, trip_price, date);
                    tp.trip_id = trip_id;
                    lst.Add(tp);
                }
                reader.Close();
            }
            con.Close();
            return lst;
        }

        public List<int> GetTripsRelatedToTrain(int train_id) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
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
            con.Close();
            return tripIDList;
        }

        public void RemoveSeats(int train_id) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();

            string query = $"DELETE FROM Seat WHERE Train_train_id={train_id}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("No Seats to delete.");
                } else {
                    Console.WriteLine("Seats related to this trip have been deleted successfully.");
                }
            }
            con.Close();
        }

        public List<int> GetAvailableSeats(int trip_id) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            List<int> seatIDList = new List<int>();  // Instantiate the list
            con.Open();
            string query = $"Select s.* from Seat s JOIN Trip t ON s.Train_train_id = t.train_id where s.state = 0 and t.trip_id = {trip_id}";
            using (SqlCommand command = new SqlCommand(query, con)) {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    int seat_id = reader.GetInt32(0);

                    seatIDList.Add(seat_id);
                }
                reader.Close();
            }
            con.Close();
            return seatIDList;
        }

        public void ConfirmBooking(List<int> seatList, Trip trip, int customerID) {
            string str = "Server = DESKTOP-FUGQNF4\\DB_SQL; Initial Catalog = Train-Booking; Integrated Security = true;";
            SqlConnection con = new SqlConnection(str);
            con.Open();
            string query = "insert into Booking(price, booking_time, trip_date, customer_id, trip_id, train_id) values (@price , @booking_time, @trip_date, @customer_id, @trip_id, @train_id)";
            using (SqlCommand command = new SqlCommand(query, con)) {
                command.Parameters.AddWithValue("@price", seatList.Count * trip.price);
                command.Parameters.AddWithValue("@booking_time", DateTime.UtcNow.ToLocalTime().ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@trip_date", trip.tripDate);
                command.Parameters.AddWithValue("@customer_id", customerID);
                command.Parameters.AddWithValue("@trip_id", trip.trip_id);
                command.Parameters.AddWithValue("@train_id", trip.train_id);
                int result = command.ExecuteNonQuery();
                if (result < 0) {
                    Console.WriteLine("Error inserting data into Database!");
                } else {
                    Console.WriteLine("Trip added successfully.");
                }
            }

            foreach (int seat in seatList) {
                query = $"UPDATE Seat SET state = 1, booking_id = (SELECT max(booking_id) from Booking) WHERE seat_id={seat}";
                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}