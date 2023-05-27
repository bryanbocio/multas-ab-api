using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.GenericRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

    }
}
