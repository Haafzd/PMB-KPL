using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB.StateMachine
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

    
