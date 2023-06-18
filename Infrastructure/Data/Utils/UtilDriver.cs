using Core.Entities.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Utils
{
    public class UtilDriver
    {


        public static Driver BuildDriverObject(string driverIdentity,string name, string lastName, string phoneNumber)
        {



            return new Driver 
            {
                DriverId= driverIdentity,
                Name= name,
                LastName= lastName,
                Number= phoneNumber,
            };
        }
    }
}
