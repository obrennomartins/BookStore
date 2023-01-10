using BookStore.Api.Configurations;
using BookStore.Data.Context;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies();

builder.Services.AddDbContext<BookStoreContext>();
builder.Services.AddHealthChecks().AddDbContextCheck<BookStoreContext>();
builder.Services.AddWebApiConfig();

builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebApiConfig();

app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    AllowCachingResponses = false
});

app.Run();