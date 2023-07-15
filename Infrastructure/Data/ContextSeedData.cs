using Core.Entities.TrafficFine;
using Infrastructure.Data.Context;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class ContextSeedData
    {
        public static async Task SeeDataAsync(TrafficDbContext context, ILoggerFactory logger)
        {
            try
            {
                await SeedTrafficFineReasonsAsync(context);

                if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();

            }
            catch (Exception exception)
            {
                var log = logger.CreateLogger<ContextSeedData>();
                log.LogError(exception.Message);
            }
        }


        private static async Task SeedTrafficFineReasonsAsync(TrafficDbContext context)
        {
            if (!context.TrafficFineReasons.Any())
            {
                var reasonsData = File.ReadAllText("../Infrastructure/Data/SeedData/TrafficFineReason.json");
                var reasons=JsonConvert.DeserializeObject<List<TrafficFineReason>>(reasonsData);

                if(reasons is not null)
                {
                    context.TrafficFineReasons.AddRange(reasons);
                }
            }
        }
    }

}
