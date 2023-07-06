using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Domain
{
    public interface ISimpleCrudRepository<T> where T : IdentifiedObject
    {
        IUnitOfWork UnitOfWork { get;   }
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task DeleteById(int id); 
    }
}
