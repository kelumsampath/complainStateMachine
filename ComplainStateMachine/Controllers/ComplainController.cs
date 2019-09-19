using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComplainStateMachine.classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ComplainStateMachine.classes.StateMachineClass;
using static ComplainStateMachine.models.stateClass;

namespace ComplainStateMachine.Controllers
{
    [Route("api/comState")]
    [ApiController]
    public class ComplainController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public complainState Post([FromBody] complainState value)
        {
             
        StateMachineClass _machineState = new StateMachineClass(value.mystate);

           

            complainState caseState;
            caseState = _machineState.moveTrigger(value.action);

            //if (string.Compare(value.action, "forward") == 0)
            //{
            //    caseState = _machineState.CaseMoveForward();
            //}else if (string.Compare(value.action, "backward") == 0)
            //{
            //     caseState = _machineState.CaseMoveBackward();
            //}else if (string.Compare(value.action, "internalforward") == 0)
            //{
            //     caseState = _machineState.CaseForwardInternal();
            //}
            //else
            //{
            //     caseState = new complainState
            //    {
            //        _cState = "action false",
            //        _newState = "action false"
            //    };
                
            //}

            return caseState;

        }

        



    }
}