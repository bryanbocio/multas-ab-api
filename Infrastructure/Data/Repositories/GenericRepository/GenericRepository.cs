using Core.Entities;
using Core.Interfaces.GenericRepository;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.GenericRepository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly TrafficDbContext _dbContext;

        public GenericRepository(TrafficDbContext dbContext)
        {
            _dbContext= dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
    }
}
