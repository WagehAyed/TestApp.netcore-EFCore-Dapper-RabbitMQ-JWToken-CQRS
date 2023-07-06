using TestApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Application.Services
{
    public interface ISimpleCrudService<T> where T: IdentifiedObject
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T item);
        Task Update(T item);
        Task DeleteById(int id);
    }
}
