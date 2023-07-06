using TestApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace TestApp.Application.Services
{
    public class SimpleCrudService<T>: ISimpleCrudService<T> where T:IdentifiedObject
    {
        private readonly ISimpleCrudRepository<T> _repository;

        public SimpleCrudService(ISimpleCrudRepository<T> repository)
        {
            repository = _repository;
        }
        public Task<IEnumerable<T>> GetAll()
        {
            return _repository.GetAll();
        }
        public Task<T> GetById(int id)
        {
            return _repository.GetById(id);
        }
        public async Task Add(T item)
        {
            await _repository.Add(item);
            await _repository.UnitOfWork.SaveChangesAsync();

        }
        public async Task Update(T item)
        {
            var entity = await _repository.GetById(item.Id);
            item.Adapt(entity);
            await _repository.UnitOfWork.SaveChangesAsync(); 
        }
        public Task DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }
    }
}
