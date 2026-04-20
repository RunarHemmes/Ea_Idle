using MyAPI.Models;

namespace MyAPI.EndPoints
{
    public static class AccountController
    {
        public static void MapAccountEndPoints(this WebApplication app)
        {
            RouteGroupBuilder accountGroup = app.MapGroup("/account");
            accountGroup.MapGet("/", Get);
        }

        private static List<Account> Get()
        {
            throw new NotImplementedException();
        }
    }
}
