namespace Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add service to the container
        var app = builder.Build();

        // Configure Http pipeline
        app.Run();
    }
}
