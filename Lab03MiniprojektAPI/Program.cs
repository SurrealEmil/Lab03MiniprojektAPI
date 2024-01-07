using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Handlers;
using Lab03MiniprojektAPI.Models;
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

            //--------------------------Person
            // Get-endpoint
            app.MapGet("/person", PersonHandler.SeeAllPeople);
            app.MapGet("/person/{personId}", PersonHandler.PersonFullInfo);

            // Post-endpoint
            app.MapPost("/person", PersonHandler.AddPerson);

            // Delete-endpoint
            app.MapDelete("/person/{personId}", PersonHandler.RemovePersonById);

            //--------------------------Interest
            // Get-endpoint
            app.MapGet("/interest", InterestHandler.SeeAllInterest);
            app.MapGet("/person/{personId}/interest", InterestHandler.SeeOnePersonInterest);

            // Post-endpoint
            app.MapPost("/interest", InterestHandler.AddInterest);
            app.MapPost("/person/{personId}/interest", InterestHandler.AddInterestToPerson);
            app.MapPost("/person/{personId}/interest/{interestId}", InterestHandler.ConnectInterestToPerson);

            // Delete-endpoint
            app.MapDelete("/interest/{interestId}", InterestHandler.RemoveInterestById);

            //--------------------------WebsiteLink
            // Get-endpoint
            app.MapGet("/person/{personId}/link", WebsiteLinkHandler.SeeOnePersonsLinks);
            
            // Post-endpoint
            app.MapPost("/person/{personId}/interest/{interestId}/link", WebsiteLinkHandler.AddURLToInterestAndPerson);

            app.Run();
        }
    }
}
