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

            return caseState;

        }

        



    }
}