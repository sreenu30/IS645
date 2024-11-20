using IS645Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;


namespace IS645Project.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string successMessage = String.Empty;
        public string errorMessage = String.Empty;
        public string email = String.Empty;
        public string password = String.Empty;
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void OnPost()
        {
            password = Request.Form["Password"];
            email = Request.Form["Email"];

            if (email.Length == 0 || password.Length == 0)
            {
                errorMessage = "All Fields are required";
            }
            else
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DBCS")))
                {
                    try
                    {
                        DAL dAL = new DAL();
                        int res = dAL.confirmLoginPassword(_configuration, password, email);
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return;
                    }

                    // Set session or cookie
                    HttpContext.Session.SetString("Email", email);

                    password = String.Empty;
                    email = String.Empty;
                    successMessage = "You're Logged in";
                    Response.Redirect("/");
                }
            }
        }

    }
}
