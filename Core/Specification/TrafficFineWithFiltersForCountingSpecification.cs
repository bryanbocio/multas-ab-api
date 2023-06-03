using Core.Entities.TrafficFine;
using Core.Specification.Parameters.TrafficFine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class TrafficFineWithFiltersForCountingSpecification : BaseSpecification<TrafficFine>
    {
        public TrafficFineWithFiltersForCountingSpecification(TrafficFineSpecificationParameters parameters) :
            base(fine =>
             (string.IsNullOrEmpty(parameters.Search) || fine.CarPlate.ToLower().Contains(parameters.Search))
              &&
             (!parameters.DriverId.HasValue || fine.DriverId== parameters.DriverId)
             &&
             (!parameters.AgentId.HasValue || fine.AgentId == parameters.AgentId)
            )
        {

        }
    }
}
