using Ea_API.Interfaces;
using Ea_API.Repositories;
using Ea_API.Services;

namespace Ea_API.IoC
{
    public static class RepositoryIoC
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IConnectionRepository, ConnectionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IGameProgressRepository, GameProgressRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            //services.AddScoped<IConnectionService, ConnectionService>();
            services.AddScoped<ISecurityService, SecurityService>();
            //services.AddScoped<IGameProgressService, GameProgressService>();
        }
    }
}
