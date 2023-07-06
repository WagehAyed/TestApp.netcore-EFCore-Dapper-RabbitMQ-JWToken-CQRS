using TestApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Infrastructure.Persistence
{
    public class SimpleCrudRepository<T>: ISimpleCrudRepository<T> where T : IdentifiedObject
    {
        public SimpleCrudRepository(DbContext context)
        {
            Context = context;
            UnitOfWork = context as IUnitOfWork;
        }
        protected DbContext Context { get; }
        public IUnitOfWork UnitOfWork { get; } 
    

 
        public async Task Add(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        public async Task DeleteById(int id)
        {
            var entity = await GetById(id);
            if (entity == null)
                Context.Set<T>().Remove(entity);
          
        }

        public Task<T> GetById(int id)
        {
            return AsQueryable().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await AsQueryable().ToListAsync();
        }
        protected virtual IQueryable<T> AsQueryable()
        {
            var queryable = Context.Set<T>().AsQueryable();

            foreach (var navigation in Context.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                queryable = queryable.Include(navigation.Name);
                queryable = IncludeChildNavigations(queryable, navigation, navigation.Name);
            }

            return queryable;
        }

        private IQueryable<T> IncludeChildNavigations(IQueryable<T> queryable, INavigation navigation, string path)
        {
            var childNavigations = navigation.TargetEntityType?.GetNavigations();
            if (childNavigations.Any())
            {
                foreach (var childNavigation in childNavigations)
                {
                    var childNavigationPath = $"{path}.{childNavigation.Name}";
                    queryable = queryable.Include(childNavigationPath);
                    queryable = IncludeChildNavigations(queryable, childNavigation, childNavigationPath);
                }
            }
            return queryable;
        }
    }
}
