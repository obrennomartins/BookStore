using BookStore.Business.Interfaces;
using BookStore.Business.Notifications;
using BookStore.Data.Context;

namespace BookStore.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ResolveDependencies(this IServiceCollection services)
    {
        services.AddScoped<BookStoreContext>();

        services.AddScoped<INotifier, Notifier>();
        
        return services;
    } 
}