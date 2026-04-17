using MyAPI.Interfaces;
using MyAPI.Repositories;

namespace MyAPI.IoC
{
    public static class RepositoryIoC
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IGameProgressRepository, GameProgressRepository>();
        }
    }
}
