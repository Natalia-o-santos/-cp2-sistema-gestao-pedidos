using AutoMapper.Extensions.Microsoft.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MottuDelivery.Application.Mappings;
using MottuDelivery.Application.Services;
using MottuDelivery.Application.Validators;

namespace MottuDelivery.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(MappingProfile));

        // FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateClienteDtoValidator>();

        // Services
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IFuncionarioService, FuncionarioService>();
        services.AddScoped<IPedidoService, PedidoService>();

        return services;
    }
}
