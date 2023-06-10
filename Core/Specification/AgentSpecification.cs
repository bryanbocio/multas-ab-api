using Core.Entities.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class AgentSpecification : BaseSpecification<Agent>
    {

        public AgentSpecification(string agentIdentity): base(agent=> agent.AgentId==agentIdentity)
        {

        }
    }
}
