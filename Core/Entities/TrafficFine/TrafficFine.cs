using Core.Enums;

namespace Core.Entities.TrafficFine
{
    public class TrafficFine : BaseEntity
    {
        public Driver.Driver Driver { get; set; }
        public int DriverId { get; set; }
        public Agent.Agent Agent { get; set; }
        public int AgentId { get; set; }
        public string CarPlate { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateCreated { get; }= DateTime.Now;
        public string Status { get; set; } = PaymentStatus.PENDIENTE.ToString();


        public TrafficFine()
        {

        }
        public TrafficFine(Driver.Driver driver, Agent.Agent agent, string carPlate, string reason, string comment, string latitude, string longitude)
        {
            Driver= driver;
            Agent= agent;
            CarPlate= carPlate;
            Reason= reason;
            Comment= comment;
            Latitude= latitude;
            Longitude= longitude;
        }
    
    }
}
