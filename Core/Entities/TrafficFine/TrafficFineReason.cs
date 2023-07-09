using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.TrafficFine
{
    public class TrafficFineReason : BaseEntity
    {
        public string Reason { get; set; }
        public decimal Price { get; set; }

    }
}
