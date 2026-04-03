using Microsoft.Extensions.DependencyInjection;

namespace TextFilter.App.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<TextFilterService>();
        return services;
    }
}
