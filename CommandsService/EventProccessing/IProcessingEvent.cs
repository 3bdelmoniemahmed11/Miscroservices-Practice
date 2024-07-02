namespace CommandsService.EventProccessing
{
    public interface IProcessingEvent
    {
        public void ProcessEvent(string message);
    }
}
