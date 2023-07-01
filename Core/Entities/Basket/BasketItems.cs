using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Basket
{
    public class BasketItems
    {
        public int Id { get; set; }
        public int TrafficFineId { get; set; }
        public decimal TrafficFinePrice { get; set; }
    }
}
