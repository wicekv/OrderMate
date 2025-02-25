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
    string? connectionString = config.GetConnectionString("SqliteConnection");
    Guard.Against.Null(connectionString);
    services.AddDbContext<AppDbContext>(options =>
     options.UseSqlite(connectionString));

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IGetUsersQueryService, GetUsersQueryService>();


    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
