namespace BookStore.Api.Configurations;

public static class ApiConfig
{
    public static IServiceCollection AddWebApiConfig(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddCors(options =>
            options.AddPolicy("Development", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            })
        );
        
        return services;
    }
    
    public static IApplicationBuilder UseWebApiConfig(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseCors("Development");
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        
        return app;
    }
}