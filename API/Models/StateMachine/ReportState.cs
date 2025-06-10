namespace API.Models.StateMachine
{
    public enum ReportState
    {
        NotStarted,
        Validating,
        Generating,
        Completed,
        Failed
    }
}
