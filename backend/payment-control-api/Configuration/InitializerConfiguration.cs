using payment_control_infrastructure.Repositories.Payment;
using payment_control_infrastructure.Repositories.Client;
using payment_control_domain.Interfaces.Repositories;
using payment_control_application.Services.Payment;
using payment_control_application.Services.Client;
using payment_control_infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace payment_control_api.Configuration;

public static class InitializerConfiguration
{
    public static IServiceCollection ConfigureInitializer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .DependencyInjectionInitializer()
            .AddControllers();
        services.ConfigureDatabase(configuration);

        return services;
    }

    public static IServiceCollection DependencyInjectionInitializer(this IServiceCollection services)
    {
        services.ServicesInjection()
            .RepositoriesInjection();

        return services;
    }

    private static IServiceCollection ServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>(); 
        services.AddScoped<IClientService, ClientService>(); 
        return services;
    }

    private static IServiceCollection RepositoriesInjection(this IServiceCollection services)
    {
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IClientRepository, ClientRepository>(); 
        return services;
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PaymentContext>(options =>
            options.UseSqlite(configuration["CONNECTIONTRINGS:PAYMENTCONTROLDB"]));
        return services;
    }

    public static void InitializeMigration(this IApplicationBuilder builder)
    {
        using (var scope =  builder.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider.GetService<PaymentContext>();
            service?.Database.Migrate();
        }
    }   
}