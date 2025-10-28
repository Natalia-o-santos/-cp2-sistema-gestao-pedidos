using AutoMapper;
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
        services.AddValidatorsFromAssemblyContaining<CreateEntregadorDtoValidator>();

        // Services
        services.AddScoped<IEntregadorService, EntregadorService>();
        services.AddScoped<IEntregaService, EntregaService>();

        return services;
    }
}
