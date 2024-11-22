namespace IS645Project.Middleware
{
    public class RedirectToLoginMiddleware
    {
        private readonly RequestDelegate _next;

        public RedirectToLoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path;

            // Check if the user is authenticated and not already on the login page
            if (path.StartsWithSegments("/Account/Login", StringComparison.OrdinalIgnoreCase) ||
            path.StartsWithSegments("/Account/Register", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Check if user session exists
            if (string.IsNullOrEmpty(context.Session.GetString("Email")))
            {
                context.Response.Redirect("/Account/Login");
                return;
            }


            // Proceed to the next middleware if authenticated or already on the login page
            await _next(context);
        }
    }
}
