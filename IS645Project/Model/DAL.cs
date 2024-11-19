using System.Data.SqlClient;
using System.Data;

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
                        r.CustomerID = (int)Convert.ToInt64(dt.Rows[i]["CustomerID"]);
                        r.RoomNumber = (int)Convert.ToInt64(dt.Rows[i]["RoomNumber"]);
                        r.CheckInDate = Convert.ToString(dt.Rows[i]["CheckInDate"]);
                        r.CheckOutDate = Convert.ToString(dt.Rows[i]["CheckOutDate"]);


                        reservations.Add(r);
                    }
                }
            }


            return reservations;
        }
        
        
        
        
        //public bool registerCustomer

    }
}
