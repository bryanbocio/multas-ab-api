using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.TrafficFine
{
    public class TrafficFine : BaseEntity
    {
        public string IdDriver { get; set; }
        public string CarPlate { get; set; }
        public string reason { get; set; }
        public string comment { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public DateTime DateCreated { get; }= DateTime.Now;

    }
}
