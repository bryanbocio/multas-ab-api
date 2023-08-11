using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.TrafficFine
{
    public class TrafficFineReason : BaseEntity
    {


        public TrafficFineReason(int id, string reason, decimal price)
        {
            Id=id;
            Reason=reason;
            Price=price;
        }

        public string Reason { get; set; }
        public decimal Price { get; set; }

    }
}
