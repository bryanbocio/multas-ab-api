using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PersonBaseEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName{ get; set; }
        public string Number { get; set; }
    }
}
