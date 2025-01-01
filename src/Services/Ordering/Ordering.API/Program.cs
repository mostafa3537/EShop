using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddApplicationServices(builder.Configuration)
            .AddInfrastructureServices(builder.Configuration)
            .AddApiServices(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseApiServices();

        if (app.Environment.IsDevelopment())
        {
            //await app.InitialiseDatabaseAsync();
        }

        app.Run();
    }
}
