using Carter;
using FluentValidation;
using Marten;

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
        });
        builder.Services.AddValidatorsFromAssembly(assembly);

        builder.Services.AddCarter();

        builder.Services.AddMarten(opts =>
        {
            opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        }).UseLightweightSessions();

        var app = builder.Build();

        // pipeline
        app.MapCarter();

        app.Run();
    }
}
