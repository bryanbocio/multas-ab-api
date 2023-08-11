using Core.Entities.TrafficFine;
using Core.Entities.Agent;
using Core.Entities.Driver;
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
                await SeedAgentAsync(context);
                await SeedDriverAsync(context);
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

         private static async Task SeedAgentAsync(TrafficDbContext context)
        {
            if (!context.Agent.Any())
            {

                var agent = new Agent(){Id=1, AgentId="302111172990",Name="Jose", LastName="Martinez", Number="8290009875"}; 

                    context.Agent.AddRange(agent);
            }
        }

        private static async Task SeedDriverAsync(TrafficDbContext context)
        {
            if (!context.Driver.Any())
            {

                var driver = new Driver(){Id=1, DriverId="40244447290", Name="Armando", LastName="Paredes", Number="8095623651"};
                context.Driver.AddRange(driver);
            }
        }
    }

}
