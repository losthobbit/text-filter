using Microsoft.Extensions.DependencyInjection;
using TextFilter.App.Application;
using TextFilter.App.Infrastructure;

var textFilterService = new ServiceCollection()
    .AddInfrastructure(args[0])
    .AddApplication()
    .BuildServiceProvider()
    .GetRequiredService<TextFilterService>();

textFilterService.Run();