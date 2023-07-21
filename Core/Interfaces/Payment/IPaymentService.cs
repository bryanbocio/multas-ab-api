using Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Payment
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    }
}
