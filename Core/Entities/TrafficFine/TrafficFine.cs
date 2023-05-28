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


    
    }
}
