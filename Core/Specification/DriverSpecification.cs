
using Core.Entities.Driver;

namespace Core.Specification
{
    public class DriverSpecification : BaseSpecification<Driver>
    {
        public DriverSpecification(string driverIdentity) : base(driver=> driver.DriverId==driverIdentity)
        {

        }
    }
}
