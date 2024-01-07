using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Lab03MiniprojektAPI.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Lab03MiniprojektAPI.Models.DTO;

namespace Lab03MiniprojektAPI.Handlers
{
    public class WebsiteLinkHandler
    {
        public static IResult SeeOnePersonsLinks(ApplicationContext context, int personId)
        {
            try
            {
                Person? person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.WebsiteLinks)
                .FirstOrDefault();

                if (person == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                var WebsiteLinkInfo = person.WebsiteLinks.Select(I => new WebsiteLinkView()
                {
                    Id = I.Id,
                    Link = I.Link
                }).ToArray();
                return Results.Json(WebsiteLinkInfo);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult AddURLToInterestAndPerson(ApplicationContext context, WebsiteLinkDto websiteLinkDto, int personId, int interestId)
        {
            try
            {
                var person = context.Persons
                .Include(p => p.WebsiteLinks)
                .FirstOrDefault(p => p.Id == personId);

                if (person == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                var interest = context.Interests.FirstOrDefault(i => i.Id == interestId);

                if (interest == null)
                {
                    return Utility.HandleNotFound("Interest", interestId);
                }

                // Check for null before creating the WebsiteLink
                if (websiteLinkDto.Link == null)
                {
                    return Results.BadRequest("Link cannot be null.");
                }

                var link = new WebsiteLink { Link = websiteLinkDto.Link, Interests = interest, Persons = person };

                person.WebsiteLinks.Add(link);
                context.SaveChanges();

                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }
    }
}
