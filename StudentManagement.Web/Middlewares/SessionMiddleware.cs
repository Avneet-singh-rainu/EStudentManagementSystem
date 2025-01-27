namespace EStudentManagement.Web.Middlewares {

    using System.Threading.Tasks;

    // File: Middleware/SessionMiddleware.cs
    using Microsoft.AspNetCore.Http;

    public class SessionMiddleware {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            var userName = httpContext.Session.GetString("UserName");
            httpContext.Items["UserName"] = userName;
            await _next(httpContext);
        }
    }
}