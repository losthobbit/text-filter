using Microsoft.Extensions.DependencyInjection;
using TextFilter.App.Application.IO;

namespace TextFilter.App.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, string filename)
    {
        services.AddSingleton<ITextReader>(_ => new FileTextReader(filename));
        services.AddSingleton<IOutputWriter, ConsoleOutputWriter>();
        return services;
    }
}