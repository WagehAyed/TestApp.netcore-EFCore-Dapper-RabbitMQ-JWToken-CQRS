using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Domain
{
    public interface IUnitOfWork :IDisposable
    {
        Task BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);
        Task Commit(CancellationToken cancellationToken = default);
        Task Rollback(CancellationToken cancellationToken = default); 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task ExecuteSql(string sql, CancellationToken cancellationToken = default);
    }
}
