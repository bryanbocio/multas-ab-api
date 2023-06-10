
using Core.Entities.Driver;
using Core.Specification.Parameters.Driver;

namespace Core.Specification
{
    public class DriverSpecification : BaseSpecification<Driver>
    {
        public DriverSpecification(string driverIdentity) : base(driver=> driver.DriverId==driverIdentity)
        {

        }

        public DriverSpecification(DriverSpecificationParameters parameters) 
            : base(driver =>

                 (string.IsNullOrEmpty(parameters.Search) || driver.Name.ToLower().Contains(parameters.Search))
                  &&
                 (string.IsNullOrEmpty(parameters.DriverId) || driver.DriverId == parameters.DriverId)
                  )
        {

        }
    }
}
