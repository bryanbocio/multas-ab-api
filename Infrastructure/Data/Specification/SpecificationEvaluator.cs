using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Specification
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> getQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spectification)
        {
            var query = inputQuery;

            if (spectification.criteria != null)
            {
                query = query.Where(spectification.criteria);
            }

            if (spectification.OrderBy != null)
            {
                query = query.OrderBy(spectification.OrderBy);
            }

            if (spectification.OrderByDescending != null)
            {
                query = query.OrderByDescending(spectification.OrderByDescending);
            }
            if (spectification.IsPagingEnabled)
            {
                query = query.Skip(spectification.Skip).Take(spectification.Take);
            }

            query = spectification.includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
