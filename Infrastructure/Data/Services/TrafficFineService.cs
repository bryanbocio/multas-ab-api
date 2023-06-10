using Core.Entities.TrafficFine;
using Core.Interfaces.Services;
using Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class TrafficFineService : ITrafficFineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrafficFineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<TrafficFine> CreateTrafficFine(string driverIdentity, string agentIdentity, string carPlate, string Reason, string comment, string latitude, string longitude, DateTime dateCreate)
        {
            throw new NotImplementedException();
        }
    }
}
