using OrderMate.Core.Common;
using OrderMate.Infrastructure.Data;
using OrderMate.Infrastructure.Data.Queries.Users.List;
using OrderMate.UseCases.Users.List;

namespace OrderMate.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {

    string? connectionString = config.GetConnectionString("DefaultConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>((sp, options) =>
    {
      options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    });

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IGetUsersQueryService, GetUsersQueryService>()
           .AddScoped<IEventStore, EfEventStore>();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
