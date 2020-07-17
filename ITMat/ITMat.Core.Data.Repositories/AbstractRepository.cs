using Dapper;
using ITMat.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Repositories
{
    public abstract class AbstractRepository<TEntity> where TEntity : AbstractModel
    {
        private readonly IsolationLevel isolationLevel = IsolationLevel.Serializable;
        private readonly string connectionString;

        protected AbstractRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbConnectionString");
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected async Task<IEnumerable<TEntity>> QueryMultipleAsync(string query, object param = null)
            => await QueryAsync(async conn => await conn.QueryAsync<TEntity>(query, param));

        protected async Task<IEnumerable<T>> QueryMultipleAsync<T>(string query, object param = null)
            => await QueryAsync(async conn => await conn.QueryAsync<T>(query, param));

        protected async Task<IEnumerable<TEntity>> QueryMultipleAsync<TSecond>(string query, Func<TEntity, TSecond, TEntity> map, object param = null)
            => await QueryAsync(async conn => await conn.QueryAsync(query, map, param));

        protected async Task<TEntity> QuerySingleAsync(string query, object param = null)
            => await QueryAsync(async conn => await conn.QuerySingleAsync<TEntity>(query, param));

        protected async Task<T> QuerySingleAsync<T>(string query, object param = null)
            => await QueryAsync(async conn => await conn.QuerySingleAsync<T>(query, param));

        protected async Task<TEntity> QuerySingleAsync<TSecond>(string query, Func<TEntity, TSecond, TEntity> map, object param = null)
            => await QueryAsync(async conn => (await conn.QueryAsync(query, map, param)).First());

        /// <summary>
        /// Executes an SQL-statement and returns number of affected rows
        /// </summary>
        /// <param name="query">The SQL-query to execute</param>
        /// <param name="param">The parameters to use for this query</param>
        /// <returns>Number of rows affected</returns>
        protected async Task<int> ExecuteAsync(string query, object param = null)
            => await QueryAsync(async conn => await conn.ExecuteAsync(query, param));

        protected async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return await func(connection);
            }
            catch (InvalidOperationException) { throw new KeyNotFoundException("Object was not found."); }
            catch (Exception e) { throw new DataException("Something went wrong.", e); }
        }

        protected async Task<T> Transaction<T>(Func<IDbTransaction, Task<T>> func)
        {
            using var connection = new SqlConnection(connectionString);
            using var transaction = connection.BeginTransaction(isolationLevel);

            try
            {
                var result = await func(transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}