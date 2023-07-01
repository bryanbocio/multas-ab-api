using Core.Entities.Agent;
using Core.Specification.Parameters.Agent;
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

        public AgentSpecification(AgentSpecificationParameter parameters)
            : base(agent=>

                (string.IsNullOrEmpty(parameters.Search) || agent.Name.ToLower().Contains(parameters.Search))
                 &&
                (string.IsNullOrEmpty(parameters.AgentId) || agent.AgentId == parameters.AgentId)

                  )
        {

        }
    }
}
