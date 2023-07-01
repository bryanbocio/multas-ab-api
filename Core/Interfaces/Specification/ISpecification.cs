using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Specification
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> criteria { get; }

        List<Expression<Func<T, object>>> includes { get; }

        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}
