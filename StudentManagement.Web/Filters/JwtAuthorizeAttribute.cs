using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EStudentManagement.Web.Filters {
    public class JwtAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            Console.WriteLine(token + "...............................");

            //if (token == null) {
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}

            //try {
            //    var key = Encoding.UTF8.GetBytes("YourSecretKeyHere");  // Replace with your secret key
            //    var handler = new JwtSecurityTokenHandler();
            //    var validationParameters = new TokenValidationParameters {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidIssuer = "yourIssuer", // Replace with your issuer
            //        ValidAudience = "yourAudience", // Replace with your audience
            //        IssuerSigningKey = new SymmetricSecurityKey(key)
            //    };

            //    handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            //}
            //catch {
            //    context.Result = new UnauthorizedResult();
            //}
        }
    }
}
