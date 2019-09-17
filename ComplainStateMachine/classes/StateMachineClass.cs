using Stateless;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static ComplainStateMachine.models.stateClass;

namespace ComplainStateMachine.classes
{
    public class StateMachineClass
    {
        public enum State
        { New,
            Waiting,
            Process,
            Finished,
            Rejected,
            Success
        }
        public enum Trigger
        {
            MoveForward,
            MoveBackward,
            ProcessInside
        }

        private readonly StateMachine<State, Trigger> _machine;

        public StateMachineClass(State _state)
        {

            _machine = new StateMachine<State, Trigger>(_state);

            _machine.Configure(State.New)
                .Permit(Trigger.MoveForward, State.Waiting);

            _machine.Configure(State.Waiting)
                .Permit(Trigger.ProcessInside, State.Process);

            _machine.Configure(State.Process)
                .Permit(Trigger.ProcessInside, State.Finished)
                .Permit(Trigger.MoveForward, State.Rejected)
                .Permit(Trigger.MoveBackward, State.Waiting);

            _machine.Configure(State.Finished)
                .Permit(Trigger.MoveForward, State.Success);

            _machine.Configure(State.Rejected)
                .Permit(Trigger.MoveBackward, State.Waiting);
        }
        
        public complainState CaseMoveForward()
        {
            String PreviousState = _machine.State.ToString();
            try
            {
                _machine.Fire(Trigger.MoveForward);
                String NewState = _machine.State.ToString();
                Debug.WriteLine("my state machine Trigger: CaseMoveForward");

                complainState cs = new complainState
                {
                    _cState = PreviousState,
                    _newState = NewState
                };
                return cs;
            }
            catch(InvalidOperationException e)
            {
                complainState cs = new complainState
                {
                    _cState = "error",
                    _newState = "error"
                };
                return cs;
            }
           
            
        }
    
public complainState CaseMoveBackward()
        {
            String PreviousState = _machine.State.ToString();
            try
            {
                _machine.Fire(Trigger.MoveBackward);
                String NewState = _machine.State.ToString();
                Debug.WriteLine("my state machine Trigger: CaseMoveBackward");

                complainState cs = new complainState
                {
                    _cState = PreviousState,
                    _newState = NewState
                };
                return cs;
            }
            catch (InvalidOperationException e)
            {
                complainState cs = new complainState
                {
                    _cState = "error",
                    _newState = "error"
                };
                return cs;
            }
        }
        public complainState CaseForwardInternal()
        {
            String PreviousState = _machine.State.ToString();
            try
            {
                _machine.Fire(Trigger.ProcessInside);
                String NewState = _machine.State.ToString();
                Debug.WriteLine("my state machine Trigger: CaseForwardInternal");

                complainState cs = new complainState
                {
                    _cState = PreviousState,
                    _newState = NewState
                };
                return cs;
            }
            catch (InvalidOperationException e)
            {
                complainState cs = new complainState
                {
                    _cState = "error",
                    _newState = "error"
                };
                return cs;
            }
        }


    }
}
