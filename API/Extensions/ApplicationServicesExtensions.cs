using Core.Interfaces.GenericRepository;
using Infrastructure.Data.GenericRepository.Repositories;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuracion)
        {

            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));


            return services;
        }
    }
}
