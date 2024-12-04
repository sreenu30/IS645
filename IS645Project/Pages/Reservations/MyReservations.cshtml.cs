using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS645Project.Model;

namespace IS645Project.Pages.Reservations
{
    public class MyReservationsModel : PageModel
    {
        private readonly IConfiguration configuration;
        public List<Reservation> reservations = new List<Reservation>();
        public List<String> roomTypes = new List<String>();
        private readonly ILogger<IndexModel> _logger;
        public MyReservationsModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }
        public void OnGet()
        {
            DAL dal = new DAL();
            reservations = dal.GetCustomerReservations(configuration, HttpContext.Session.GetString("Email"));
            for (int i = 0; i< reservations.Count; i++) { 
                roomTypes.Add(dal.GetRoom(configuration, reservations[i].RoomNumber).roomType);
            }
        }
    }
}
