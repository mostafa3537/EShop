using Carter;
using FluentValidation;

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

        var app = builder.Build();

        app.MapCarter();

        app.Run();
    }
}
