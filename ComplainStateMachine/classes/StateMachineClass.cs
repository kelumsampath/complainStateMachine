using Stateless;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainStateMachine.classes
{
    public class StateMachineClass
    {
        public enum State
        {
            Waiting,
            View,
            Closed
        }
        public enum Trigger
        {
            Preview,
            Finish
        }

        private readonly StateMachine<State, Trigger> _machine;

        public StateMachineClass(State _state)
        {

            _machine = new StateMachine<State, Trigger>(_state);

            _machine.Configure(State.Waiting)
                .Permit(Trigger.Preview, State.View);

            _machine.Configure(State.Closed)
                .Permit(Trigger.Finish, State.Closed);
        }

        public void viewcase()
        {
            _machine.Fire(Trigger.Preview);
            Debug.WriteLine("my state machine Trigger: Trigger.Preview");
            
        }
        public void finishcase()
        {
            _machine.Fire(Trigger.Finish);
            Debug.WriteLine("my state machine Trigger: Trigger.Finish");
        }
    }
}
