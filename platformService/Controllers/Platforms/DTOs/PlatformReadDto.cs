namespace platformService.Controllers.Platforms.DTOs
{
    public record PlatformReadDto(Guid Id, string Name, string Publisher, string Cost);

}
