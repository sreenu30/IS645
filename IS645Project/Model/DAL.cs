using System.Data.SqlClient;

namespace IS645Project.Model
{
    public class DAL
    {
        public List<Rooms> getRooms(IConfiguration configuration) {
            List<Rooms> rooms = new List<Rooms>();
            using (SqlConnection conn = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("", conn);
            }



                return rooms;
        }
    }
}
