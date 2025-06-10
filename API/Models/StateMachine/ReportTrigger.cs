namespace API.Models.StateMachine
{
    public enum ReportTrigger
    {
        StartGeneration,
        ValidationSuccess,
        ValidationFailed,
        GenerationSuccess,
        GenerationFailed
    }
}
