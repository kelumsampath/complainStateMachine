using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ComplainStateMachine.classes.StateMachineClass;

namespace ComplainStateMachine.models
{
    public class stateClass
    {
        public class complainState
        {
            public String _cState { get; set; }
            public String _newState { get; set; }
            public String action { get; set; }
            public String mystate { get; set; }

        }
    }
}
