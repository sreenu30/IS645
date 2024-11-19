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

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void OnGet()
        {
            DAL dal = new DAL();
            rooms = dal.getRooms(configuration);
        }
    }
}
