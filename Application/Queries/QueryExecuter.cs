using TestApp.Application.Queries;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace TestApp.Application.Queries
{
    public class QueryExecuter : IQueryExecuter
    {
        private readonly QueryExecuterOptions _options;
        public QueryExecuter(QueryExecuterOptions options)
        {
             _options= options;
        }
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryFirstOrDefaultAsync<T>(sql, param, null, commandTimeout, commandType), param, connectionString);
        }

        private async Task<T> ExecuteQueryAsync<T>(string sql, Func<IDbConnection, Task<T>> executerFunc, object param, string connectionString = null, params object[] queryStrings)
        {
            var stopwatch = Stopwatch.StartNew();
            using (var connection = new SqlConnection(connectionString ?? _options.ConnectionString))
            {
                try
                {
                    return await executerFunc(connection);
                }
                finally
                {
                    stopwatch.Stop();
                    //todo..log execution time
                }
            }
        }
    }
}
