using IS645Project.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace IS645Project.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public Customer customer = new Customer();
        public string successMessage = String.Empty;
        public string errorMessage = String.Empty;
        public string password = String.Empty;
        public string password2 = String.Empty;

        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void OnPost()
        {
            customer.FirstName = Request.Form["FirstName"];
            customer.LastName = Request.Form["LastName"];
            customer.Email = Request.Form["Email"];
            password = Request.Form["Password"];
            password2 = Request.Form["Password2"];
            if(!String.Equals(password, password2))
            {
                errorMessage = "Passwords not matching";
            } else if(customer.FirstName.Length == 0 || customer.LastName.Length == 0 || customer.Email.Length == 0 || password.Length  == 0 || password2.Length == 0) {
                errorMessage = "All Fields are required";
            }
            else
            {
                try
                {
                    DAL dAL = new DAL();
                    int i = dAL.registerCustomer(_configuration, customer, password);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }

                customer.FirstName = String.Empty;
                customer.LastName = String.Empty;
                customer.Email = String.Empty;
                password = String.Empty;
                password2 = String.Empty;
                successMessage = "Customer created";
                Response.Redirect("/Account/Login");
            }
        }
    }
}
