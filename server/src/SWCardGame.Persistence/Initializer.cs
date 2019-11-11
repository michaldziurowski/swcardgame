using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWCardGame.Core.Interfaces;

namespace SWCardGame.Persistence
{
    public static class Initializer
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SWCardGameContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("SWCardGameContext"))
                );

            services.AddScoped<ICardsRepository, CardsRepository>();
            services.AddScoped<TestDataProvider>();
        }
    }
}