using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;

namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = typeof(Program).Assembly;
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddCarter();

        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        if (builder.Environment.IsDevelopment())
            builder.Services.InitializeMartenWith<CatalogInitialData>();

        builder.Services.AddExceptionHandler<CustomExceptionHandler>();

        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", policy =>
            {
                policy.AllowAnyOrigin() // Allow all origins
                      .AllowAnyHeader() // Allow all headers
                      .AllowAnyMethod(); // Allow all HTTP methods
            });
        });

        var app = builder.Build();

        // Use CORS
        app.UseCors("AllowAllOrigins");

        // pipeline
        app.MapCarter();

        app.Run();
    }
}
