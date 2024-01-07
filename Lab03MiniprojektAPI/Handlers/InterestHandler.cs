using Lab03MiniprojektAPI.Data;
using Lab03MiniprojektAPI.Models.ViewModels;
using Lab03MiniprojektAPI.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Lab03MiniprojektAPI.Models.DTO;

namespace Lab03MiniprojektAPI.Handlers
{
    public class InterestHandler
    {
        public static IResult AddInterest(ApplicationContext context, InterestDto interestDto)
        {
            try
            {
                var interest = new Interest
                {
                    Title = interestDto.Title,
                    Description = interestDto.Description,
                };

                context.Interests.Add(interest);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult RemoveInterestById(ApplicationContext context, int interestId)
        {
            try
            {
                var interestToRemove = context.Interests.Find(interestId);

                if (interestToRemove == null)
                {
                    return Utility.HandleNotFound("Interest", interestId);
                }

                context.Interests.Remove(interestToRemove);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult SeeAllInterest(ApplicationContext context)
        {
            try
            {
                return Results.Json(context.Interests
                .Select(i => new { i.Id, i.Title }).ToArray());
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult AddInterestToPerson(ApplicationContext context, InterestDto interestDto, int personId)
        {
            try
            {
                var person = context.Persons
                .Include(p => p.Interests)
                .FirstOrDefault(p => p.Id == personId);

                if (person == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                var interest = new Interest
                {
                    Title = interestDto.Title,
                    Description = interestDto.Description,
                };

                person.Interests.Add(interest);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult ConnectInterestToPerson(ApplicationContext context, int personId, int interestId)
        {
            try
            {
                Person? person = context.Persons
                .Include(p => p.Interests)
                .FirstOrDefault(p => p.Id == personId);

                if (person == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                Interest? interest = context.Interests
                    .Where(i => i.Id == interestId)
                    .FirstOrDefault();

                if (interest == null)
                {
                    return Utility.HandleNotFound("Interest", interestId);
                }

                person.Interests.Add(interest);
                context.SaveChanges();
                return Results.StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }

        public static IResult SeeOnePersonInterest(ApplicationContext context, int personId)
        {
            try
            {
                Person? person = context.Persons
                .Where(p => p.Id == personId)
                .Include(p => p.Interests)
                .FirstOrDefault();

                if (person == null)
                {
                    return Utility.HandleNotFound("Person", personId);
                }

                var InterestInfo = person.Interests.Select(I => new InterestView()
                {
                    Id = I.Id,
                    Title = I.Title,
                    Description = I.Description,
                }).ToArray();
                return Results.Json(InterestInfo);
            }
            catch (Exception ex)
            {
                return Utility.HandleException(ex);
            }
        }
    }
}
