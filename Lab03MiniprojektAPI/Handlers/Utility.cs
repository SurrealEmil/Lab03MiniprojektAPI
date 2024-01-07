namespace Lab03MiniprojektAPI.Handlers
{
    public class Utility
    {
        public static IResult HandleException(Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }

        public static IResult HandleNotFound(string entity, int entityId)
        {
            return Results.NotFound($"{entity} with ID {entityId} not found.");
        }
    }
}
