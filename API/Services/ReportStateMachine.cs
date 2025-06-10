using API.Models.StateMachine;

namespace API.Services
{
    public class ReportStateMachine
    {
        public ReportState CurrentState { get; private set; }

        public ReportStateMachine()
        {
            CurrentState = ReportState.NotStarted;
        }

        public void Fire(ReportTrigger trigger)
        {
            switch (CurrentState)
            {
                case ReportState.NotStarted when trigger == ReportTrigger.StartGeneration:
                    CurrentState = ReportState.Validating;
                    break;

                case ReportState.Validating when trigger == ReportTrigger.ValidationSuccess:
                    CurrentState = ReportState.Generating;
                    break;

                case ReportState.Validating when trigger == ReportTrigger.ValidationFailed:
                    CurrentState = ReportState.Failed;
                    break;

                case ReportState.Generating when trigger == ReportTrigger.GenerationSuccess:
                    CurrentState = ReportState.Completed;
                    break;

                case ReportState.Generating when trigger == ReportTrigger.GenerationFailed:
                    CurrentState = ReportState.Failed;
                    break;

                default:
                    throw new InvalidOperationException($"Transisi tidak valid dari {CurrentState} dengan trigger {trigger}");
            }
        }
    }
}