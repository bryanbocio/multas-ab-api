using Core.Entities.Driver;
using Core.Entities.TrafficFine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ITrafficFineService
    {
        Task<TrafficFine> CreateTrafficFine(string driverIdentity, string agentIdentity, string carPlate, string Reason, string comment, string latitude, string longitude, DateTime dateCreate);
        Task SwitchThePaymentStatusToATrafficFineAlreadyPaid(int trafficFineId);

        Task<TrafficFine> CreateTrafficFine(TrafficFine trafficFine, Driver driver, string agentIdentity);
    }
}
