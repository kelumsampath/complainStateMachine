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

        public List<ActionModel> Trigger;

        private readonly StateMachine<String, String> _machine;

        public StateMachineClass(String _state)
        {
            WorkFlowConfiguration workFlowActionData = JsonConvert.DeserializeObject<WorkFlowConfiguration>(File.ReadAllText(@"./models/jsondata.json"));
           

            _machine = new StateMachine<String, String>(_state);

            foreach (WorkFlowActionModel dataSet in workFlowActionData.WorkFlowAction)
            {
                _machine.Configure(dataSet.StateId.ToString())
                  .Permit(dataSet.ActionId.ToString(), dataSet.AfterActionStateId.ToString());
            }

            Trigger = workFlowActionData.actions;

        
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

        }
}
