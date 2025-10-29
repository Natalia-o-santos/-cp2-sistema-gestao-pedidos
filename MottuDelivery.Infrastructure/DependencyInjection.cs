using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MottuDelivery.Domain.Interfaces;
using MottuDelivery.Infrastructure.Data;
using MottuDelivery.Infrastructure.Repositories;

namespace MottuDelivery.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<MottuDeliveryDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Repositories
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
        services.AddScoped<IPedidoRepository, PedidoRepository>();

        return services;
    }
}
