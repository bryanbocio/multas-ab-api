using Core.Entities.TrafficFine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class TrafficFineReasonSpecification : BaseSpecification<TrafficFineReason>
    {

        public TrafficFineReasonSpecification(string reasonDescription) : base(reason=>
            
            (string.IsNullOrEmpty(reasonDescription) || reason.Reason.ToUpper().Contains(reasonDescription)) 

            )
        {

        }


    }
}
