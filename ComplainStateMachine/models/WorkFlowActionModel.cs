using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplainStateMachine.models
{
    public class WorkFlowActionModel
    {
        public int StateId { get; set; }
        public string ActionId { get; set; }
        public int AfterActionStateId { get; set; }
    }
}
