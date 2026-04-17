using Ea_API.Interfaces;
using Ea_API.Repositories;

namespace Ea_API.IoC
{
    public static class RepositoryIoC
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IGameProgressRepository, GameProgressRepository>();
        }
    }
}
