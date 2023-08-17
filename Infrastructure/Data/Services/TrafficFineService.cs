using Core.Entities.Agent;
using Core.Entities.Driver;
using Core.Entities.TrafficFine;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Interfaces.UnitOfWork;
using Core.Specification;

namespace Infrastructure.Data.Services
{
    public class TrafficFineService : ITrafficFineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrafficFineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TrafficFine> CreateTrafficFine(string driverIdentity, string agentIdentity, string carPlate, string reason, string comment, string latitude, string longitude, DateTime dateCreate)
        {
            var driver = await  _unitOfWork.Repository<Driver>().GetEntityWithSpecification(new DriverSpecification(driverIdentity));
          
            var agent  = await  _unitOfWork.Repository<Agent>().GetEntityWithSpecification(new AgentSpecification(agentIdentity: agentIdentity));

            var trafficFine = new TrafficFine
                (
                   driver,
                   agent,
                   carPlate,
                   reason,
                   comment,
                   latitude,
                   longitude
                );

            _unitOfWork.Repository<TrafficFine>().Add(trafficFine);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;


            return trafficFine;
        }

        public async Task<TrafficFine> CreateTrafficFine(TrafficFine trafficFine, Driver driver, string agentIdentity)
        {
            var agent  = await  _unitOfWork.Repository<Agent>().GetEntityWithSpecification(new AgentSpecification(agentIdentity: agentIdentity));
           
            trafficFine.Driver=driver;
            trafficFine.Agent=agent;
       
            _unitOfWork.Repository<TrafficFine>().Add(trafficFine);
            
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return trafficFine;
        }

        public async Task SwitchThePaymentStatusToATrafficFineAlreadyPaid(int trafficFineId)
        {
            var trafficFine= await _unitOfWork.Repository<TrafficFine>().GetByIdAsync(trafficFineId);

            if(trafficFine is not null)
            {
                trafficFine.Status = PaymentStatus.PAGADO.ToString();
                await _unitOfWork.Complete();
            }


        }
    }
}
