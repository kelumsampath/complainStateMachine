using ComplainStateMachine.models;
using Newtonsoft.Json;
using Stateless;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ComplainStateMachine.models.stateClass;

namespace ComplainStateMachine.classes
{
    public class StateMachineClass
    {
        //public enum State
        //{ New,
        //    Waiting,
        //    Process,
        //    Finished,
        //    Rejected,
        //    Success
        //}



        //public enum Trigger
        //{
        //    MoveForward,
        //    MoveBackward,
        //    ProcessInside
        //}

        //public String[] Trigger = new String[]
        //    {
        //    "101",
        //    "102",
        //    "103"
        //    };

        //public String[] State = new String[]
        //    {
        //    "New",
        //    "Waiting",
        //    "Process",
        //    "Finished",
        //    "Rejected",
        //    "Success"
        //    };

        public List<ActionModel> Trigger;

        private readonly StateMachine<String, String> _machine;

        public StateMachineClass(String _state)
        {
            WorkFlowConfiguration workFlowActionData = JsonConvert.DeserializeObject<WorkFlowConfiguration>(File.ReadAllText(@"./models/jsondata.json"));
            //Console.WriteLine(workFlowActionData.WorkFlowAction[1].ActionId);

            _machine = new StateMachine<String, String>(_state);

            foreach (WorkFlowActionModel dataSet in workFlowActionData.WorkFlowAction)
            {
                //Console.Write(dataSet.StateId.ToString());
                //Console.Write(dataSet.ActionId.ToString());
                //Console.Write(dataSet.AfterActionStateId.ToString());
                _machine.Configure(dataSet.StateId.ToString())
                  .Permit(dataSet.ActionId.ToString(), dataSet.AfterActionStateId.ToString());
            }

            Trigger = workFlowActionData.actions;

            //_machine.Configure("0")
            //    .Permit("101", "1");

            //_machine.Configure(State[1])
            //    .Permit(Trigger[2], State[2]);

            //_machine.Configure(State[2])
            //    .Permit(Trigger[2], State[4])
            //    .Permit(Trigger[0], State[5])
            //    .Permit(Trigger[1], State[1]);

            //_machine.Configure(State[3])
            //    .Permit(Trigger[0], State[5]);

            //_machine.Configure(State[4])
            //    .Permit(Trigger[1], State[2]);
        }

        public complainState moveTrigger(string action)
        {
            foreach (ActionModel actionsData in Trigger)
            {
               if(string.Compare(actionsData.actionName, action) == 0)
                {
                    String PreviousState = _machine.State.ToString();
                    try
                    {
                        _machine.Fire(actionsData.actionId);
                        String NewState = _machine.State.ToString();
                        //Debug.WriteLine("my state machine Trigger: CaseMoveBackward");

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
            complainState ncs = new complainState
            {
                _cState = "action false",
                _newState = "action false"
            };
            return ncs;
        }



            //public complainState CaseMoveBackward()
            //{
            //    String PreviousState = _machine.State.ToString();
            //    try
            //    {
            //        _machine.Fire(Trigger[1].actionId);
            //        String NewState = _machine.State.ToString();
            //        Debug.WriteLine("my state machine Trigger: CaseMoveBackward");

            //        complainState cs = new complainState
            //        {
            //            _cState = PreviousState,
            //            _newState = NewState
            //        };
            //        return cs;
            //    }
            //    catch (InvalidOperationException e)
            //    {
            //        complainState cs = new complainState
            //        {
            //            _cState = "error",
            //            _newState = "error"
            //        };
            //        return cs;
            //    }
            //}
            //public complainState CaseForwardInternal()
            //{
            //    String PreviousState = _machine.State.ToString();
            //    try
            //    {
            //        _machine.Fire(Trigger[2].actionId);
            //        String NewState = _machine.State.ToString();
            //        Debug.WriteLine("my state machine Trigger: CaseForwardInternal");

            //        complainState cs = new complainState
            //        {
            //            _cState = PreviousState,
            //            _newState = NewState
            //        };
            //        return cs;
            //    }
            //    catch (InvalidOperationException e)
            //    {
            //        complainState cs = new complainState
            //        {
            //            _cState = "error",
            //            _newState = "error"
            //        };
            //        return cs;
            //    }
            //}

            //public complainState CaseMoveForward()
            //{
            //    String PreviousState = _machine.State.ToString();
            //    try
            //    {
            //        _machine.Fire(Trigger[0].actionId);
            //        String NewState = _machine.State.ToString();
            //        Debug.WriteLine("my state machine Trigger: CaseMoveForward");

            //        complainState cs = new complainState
            //        {
            //            _cState = PreviousState,
            //            _newState = NewState
            //        };
            //        return cs;
            //    }
            //    catch (InvalidOperationException e)
            //    {
            //        complainState cs = new complainState
            //        {
            //            _cState = "error",
            //            _newState = "error"
            //        };
            //        Console.WriteLine(e);
            //        return cs;
            //    }
            //}
        }
}
