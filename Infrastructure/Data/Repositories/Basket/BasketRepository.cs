using Core.Entities.Basket;
using Core.Interfaces.Reporitories.BasketRepository;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data.Repositories.Basket
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis )
        {
            _database= redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var oldBasketItems= await GetBasketAsync(basket.Id);

            foreach(var item in basket.Items){
                oldBasketItems.Items.Add(item);
            }
            

            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(value: oldBasketItems), TimeSpan.FromDays(30));

            return !created ? null : await GetBasketAsync(basket.Id);
        }
    }
}
