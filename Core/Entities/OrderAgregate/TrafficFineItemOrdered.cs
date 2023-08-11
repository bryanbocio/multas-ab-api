
namespace Core.Entities.OrderAgregate
{
    public  class TrafficFineItemOrdered 
    {
        public TrafficFineItemOrdered()
        {

        }


        public TrafficFineItemOrdered(int trafficFineId, string trafficFineReason)
        {
            TrafficFineId = trafficFineId;
            TrafficFineReason = trafficFineReason;
        }


        public int TrafficFineId { get; set; }
        public string TrafficFineReason { get; set; }


    }
}
