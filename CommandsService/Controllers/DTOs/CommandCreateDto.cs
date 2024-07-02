namespace CommandsService.Controllers.DTOs
{
    public record CommandCreateDto(Guid platformId , string HowTo, string CommandLine )
    {


    }
}
