using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Utils
{
    public class UtilsTrafficFineReasons
    {
        private UtilsTrafficFineReasons()
        {

        }

        public static string GetReasonCode(string description)
        {
            return description.Substring(0, 3);
        }
    }
}
