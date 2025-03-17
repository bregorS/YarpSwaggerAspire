using Gateway;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Yarp.ReverseProxy.Swagger;
using Yarp.ReverseProxy.Swagger.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var configuration = builder.Configuration.GetSection("ReverseProxy");
builder.Services
    .AddReverseProxy()
    .LoadFromConfig(configuration)
    .AddSwagger(configuration);

builder.Services.AddSwaggerGen(x =>
{
    x.UseAllOfToExtendReferenceSchemas();
    x.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "Admin API",
        Description = "An API for Admin",
        Version = "1.0"
    });
});

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var config = app.Services.GetRequiredService<IOptionsMonitor<ReverseProxyDocumentFilterConfig>>().CurrentValue;
            options.ConfigureSwaggerEndpoints(config);
        });
    }

    app.MapReverseProxy();

    app.UseHttpsRedirection();

    app.MapDefaultEndpoints();

    app.Run();
}