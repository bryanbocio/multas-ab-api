namespace API.DTOs
{
    public class TrafficFineToReturnDto
    {
        public int Id { get; set; }
        public string DriverIdentity { get; set; }
        public string DriverName { get; set; }
        public string DriverPhoneNumber { get; set; }
        public string CarPlate { get; set; }
        public string Reason { get; set; }
        public string Comment { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime DateCreated { get; set; }
        public string AgentIdentity { get; set; }
        public string Status { get; set; }

    }
}
