using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainStateMachine.models
{
    public class WorkFlowConfiguration
    {
        public List<ActionModel> actions { get; set; }
        public List<StateModels> states { get; set; }
        public List<WorkFlowActionModel> WorkFlowAction { get; set; }
    }
}
