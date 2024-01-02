using Lab03MiniprojektAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab03MiniprojektAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("ApplicationContext");

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
