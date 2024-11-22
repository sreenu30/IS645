using IS645Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IS645Project.Pages.Reservations
{
    public class BookModel : PageModel
    {
        public int roomNumber = 0;
        private Reservation reservation = new Reservation();
        public DateTime checkIn = DateTime.MaxValue;
        public DateTime checkOut = DateTime.MinValue;
        public string errorMessage = String.Empty;
        private IConfiguration configuration;
        private ILogger<IndexModel> _logger;

        public BookModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }
        public void OnGet()
        {
            roomNumber = (int)HttpContext.Session.GetInt32("RoomNumber");
        }

        public void OnPost()
        {
            DAL dAL = new DAL();

            roomNumber = (int)HttpContext.Session.GetInt32("RoomNumber");
            checkIn = DateTime.Parse(Request.Form["CheckIn"]);
            checkOut = DateTime.Parse(Request.Form["CheckOut"]);

            if (checkOut < checkIn)
            {
                errorMessage= "Check-out date must be later than or equal to the check-in date.";
                return;
            }

            reservation.RoomNumber = roomNumber;
            
            reservation.Email = HttpContext.Session.GetString("Email");
            reservation.CheckInDate = checkIn.ToString();
            reservation.CheckOutDate = checkOut.ToString();
            dAL.createReservation(configuration, reservation);
            Response.Redirect("/Reservations/MyReservations");
        }
    }
}
