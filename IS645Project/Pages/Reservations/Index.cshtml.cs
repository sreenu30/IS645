using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS645Project.Model;

namespace IS645Project.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;
        public List<Room> rooms = new List<Room>();
        public List<Reservation> reservations = new List<Reservation>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this.configuration = configuration;
            _logger = logger;
        }
        public void OnGet()
        {
            DAL dal = new DAL();
            rooms = dal.getRooms(configuration);
        }

        public IActionResult OnGetBook(int roomNumber, string roomType)
        {
            // Store room details in session
            HttpContext.Session.SetInt32("RoomNumber", roomNumber);

            // Redirect to the booking page
            return RedirectToPage("/Reservations/Book");
        }
    }
}
