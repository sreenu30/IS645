using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace IS645Project.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public IActionResult OnPost()
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DBCS")))
            {
                string query = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@Password", Password); // Hash in production

                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        // Set session or cookie
                        HttpContext.Session.SetString("Username", Username);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        Message = "Invalid username or password";
                    }
                }
            }

            return Page();
        }
    }

}
