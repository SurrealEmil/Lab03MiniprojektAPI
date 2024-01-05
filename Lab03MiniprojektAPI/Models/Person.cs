using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Lab03MiniprojektAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<WebsiteLink> WebsiteLinks { get; set; }

        public static IResult AddPerson(ApplicationContext context, Person person)
        {
            try
            {
                context.Persons.Add(person);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static IResult RemovePersonById(ApplicationContext context, int personId)
        {
            try
            {
                var personToRemove = context.Persons.Find(personId);

                if (personToRemove == null)
                {
                    return Results.NotFound($"Person with ID {personId} not found.");
                }

                context.Persons.Remove(personToRemove);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static IResult SeeAllPeople(ApplicationContext context)
        {
            return Results.Json(context.Persons
                .Select(p => new { p.Id, p.FirstName }).ToArray());
        }

        public static IResult PersonFullInfo(ApplicationContext context, int personId)
        {
            Person? person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Interests)
                .Include(p => p.WebsiteLinks)
                .FirstOrDefault();

            if (person == null)
            {
                return Results.NotFound();
            }

            var personInfo = new PersonView()
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = person.PhoneNumber,
                Interests = person.Interests.Select(i => new InterestView { Id = i.Id, Title = i.Title, Description = i.Description }).ToArray(),
                WebsiteLinks = person.WebsiteLinks.Select(i => new WebsiteLinkView { Id = i.Id, Link = i.Link }).ToArray()
            };
            return Results.Json(personInfo);
        }
    }
}
