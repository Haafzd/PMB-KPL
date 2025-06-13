namespace API.Models.StateMachine
{
    public enum LoginState
    {
        NotLoggedIn,
        WaitingCredentials,
        Authenticated,
        Failed,
        Locked
    }
}