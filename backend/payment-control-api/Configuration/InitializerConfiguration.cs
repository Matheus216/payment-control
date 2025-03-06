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
        return services;
    }

    private static IServiceCollection RepositoriesInjection(this IServiceCollection services)
    {
        return services;
    }
}