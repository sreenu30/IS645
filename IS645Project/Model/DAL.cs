using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Configuration;

namespace IS645Project.Model
{
    public class DAL
    {
        public List<Room> getRooms(IConfiguration configuration) {
            List<Room> rooms = new List<Room>();
            List<Reservation> reservations = GetReservations(configuration);

            try
            {
                using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
                {
                    string procedure = "SelectAllRooms";
                    SqlDataAdapter da = new SqlDataAdapter(procedure, conn);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Room room = new Room();
                            room.roomNumber = (int)Convert.ToInt64(dt.Rows[i]["RoomNumber"]);
                            room.roomType = Convert.ToString(dt.Rows[i]["RoomType"]);

                            if (reservations.Any(r => r.RoomNumber == room.roomNumber))
                            {
                                room.isReserved = true;
                            }
                            else
                            {
                                room.isReserved = false;
                            }
                            rooms.Add(room);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                // Log the general exception message or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
            }



            return rooms;
        }

        public List<Reservation> GetReservations(IConfiguration configuration)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "GetReservations";
                SqlDataAdapter da = new SqlDataAdapter(procedure, conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reservation r = new Reservation();
                        r.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        r.RoomNumber = (int)Convert.ToInt64(dt.Rows[i]["RoomNumber"]);
                        r.CheckInDate = Convert.ToString(dt.Rows[i]["CheckInDate"]);
                        r.CheckOutDate = Convert.ToString(dt.Rows[i]["CheckOutDate"]);


                        reservations.Add(r);
                    }
                }
            }


            return reservations;
        }
        
        public Customer GetCustomer(IConfiguration configuration, string email)
        {
            Customer customer = new Customer();


            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "GetCustomer";
                using (var cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer.FirstName = reader["Fname"].ToString();
                            customer.LastName = reader["Lname"].ToString();
                            customer.Email = reader["Email"].ToString();
                            // Use dbUsername and dbPassword as needed
                        }
                        else
                        {
                            // Handle no user found
                            customer.FirstName = "NotFound";
                        }
                    }
                    conn.Close();
                }
            }
            return customer;
        }

        public int createReservation(IConfiguration configuration, Reservation reservation)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "CreateReservation";
                using (var cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", reservation.Email);
                    cmd.Parameters.AddWithValue("@RoomNumber", reservation.RoomNumber);
                    cmd.Parameters.AddWithValue("@CheckInDate", reservation.CheckInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", reservation.CheckOutDate);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return result;
        }

        public List<Reservation> GetCustomerReservations(IConfiguration configuration, String email)
        {
            List<Reservation> reservations = new List<Reservation>();

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "GetCustomerReservations";
                SqlDataAdapter da = new SqlDataAdapter(procedure, conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Email", email);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Reservation r = new Reservation();
                        r.Email = Convert.ToString(dt.Rows[i]["Email"]);
                        r.RoomNumber = (int)Convert.ToInt64(dt.Rows[i]["RoomNumber"]);
                        r.CheckInDate = Convert.ToString(dt.Rows[i]["CheckInDate"]);
                        r.CheckOutDate = Convert.ToString(dt.Rows[i]["CheckOutDate"]);


                        reservations.Add(r);
                    }
                }
            }


            return reservations;
        }
        public int registerCustomer(IConfiguration configuration, Customer customer, string password)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "CreateCustomer";
                using (var cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return result;
        }

        //1 Means passwords match, 3 means the passwords don't match, 4 means the user does not exist
        public int confirmLoginPassword(IConfiguration configuration, string password, string email)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                string procedure = "GetCustomerPassword";

                using (var cmd = new SqlCommand(procedure, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string realPass = reader["Password"].ToString();
                            if (realPass != password)
                            {
                                throw new Exception("Wrong Password");
                            } else
                            {
                                result = 1;
                            }
                        }
                        else
                        {
                            // Handle no user found
                            throw new Exception("User not registered");
                        }
                    }
                    conn.Close();
                }
            }

            return result;
        }
    }
}
