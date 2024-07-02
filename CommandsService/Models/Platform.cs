﻿namespace CommandsService.Models
{
    public class Platform
    {
        public Guid Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Name { get; set; }
        public List<Command> Commands { get; set; } =new List<Command> ();
    }
}
