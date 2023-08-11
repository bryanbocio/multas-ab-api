using Core.Entities.TrafficFine;
using Core.Specification.Parameters.TrafficFine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class TrafficFineWithDriverAndAgentSpecification : BaseSpecification<TrafficFine>
    {

        public TrafficFineWithDriverAndAgentSpecification(TrafficFineSpecificationParameters parameters) 
            : base(fine=>

                (string.IsNullOrEmpty(parameters.Search) || fine.Reason.ToLower().Contains(parameters.Search))
                 &&
                (!parameters.DriverId.HasValue || fine.DriverId == parameters.DriverId)
                &&
                (!parameters.AgentId.HasValue || fine.AgentId == parameters.AgentId)
                 &&
                (!parameters.TrafficFineId.HasValue || fine.Id == parameters.TrafficFineId)
                )

        {

            AddInclude(fine => fine.Driver);
            AddInclude(fine => fine.Agent);
        }

        public TrafficFineWithDriverAndAgentSpecification(int id) : base(fine => fine.Id == id)
        {
            AddInclude(fine => fine.Driver);
            AddInclude(fine=> fine.Agent);
        }
    }
}
