using Core.Entities;
using Core.Interfaces.GenericRepository;

namespace Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

        Task<int> Complete();
    }
}
