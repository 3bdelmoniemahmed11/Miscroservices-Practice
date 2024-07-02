namespace CommandsService.Controllers.DTOs
{
    public record CommandReadDto ( 
         Guid Id,
         string HowTo,
         string CommandLine ,
         Guid PlatformId )
    {
    }
}

