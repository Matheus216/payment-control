using payment_control_infrastructure.Repositories.Payment;
using payment_control_infrastructure.Repositories.Client;
using payment_control_domain.Interfaces.Repositories;
using payment_control_application.Services.Payment;
using payment_control_application.Services.Client;

namespace payment_control_api.Configuration;

public static class InitializerConfiguration
{
    public static IServiceCollection ConfigureInitializer(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .DependencyInjectionInitializer()
            .AddControllers();
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
        services.AddSingleton<IPaymentService, PaymentService>(); 
        services.AddSingleton<IClientService, ClientService>(); 
        return services;
    }

    private static IServiceCollection RepositoriesInjection(this IServiceCollection services)
    {
        services.AddSingleton<IPaymentRepository, PaymentRepository>();
        services.AddSingleton<IClientRepository, ClientRepository>(); 
        return services;
    }
}