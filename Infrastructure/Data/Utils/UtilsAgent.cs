using Core.Entities.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Utils
{
    public class UtilsAgent
    {
        private UtilsAgent()
        {

        }
        public static Agent BuildAgentObject(string agentIdentity, string name, string lastName, string phoneNumber)
        {
            return new Agent
            {
                AgentId = agentIdentity,
                Name = name,
                LastName = lastName,
                Number = phoneNumber,
            };
        }
    }
}
