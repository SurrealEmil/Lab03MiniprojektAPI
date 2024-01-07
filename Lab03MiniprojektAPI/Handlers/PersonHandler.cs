using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Lab03MiniprojektAPI.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Lab03MiniprojektAPI.Models.DTO;

namespace Lab03MiniprojektAPI.Handlers
{
    public class PersonHandler
    {
        public static IResult AddPerson(ApplicationContext context, PersonDto personDto)
        {
            try
            {
                var person = new Person
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    PhoneNumber = personDto.PhoneNumber,
                };

                context.Persons.Add(person);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult RemovePersonById(ApplicationContext context, int personId)
        {
            try
            {
                var personToRemove = context.Persons.Find(personId);

                if (personToRemove == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                context.Persons.Remove(personToRemove);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult SeeAllPeople(ApplicationContext context)
        {
            try
            {
                return Results.Json(context.Persons
                .Select(p => new { p.Id, p.FirstName }).ToArray());
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult PersonFullInfo(ApplicationContext context, int personId)
        {
            try
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
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }
    }
}
