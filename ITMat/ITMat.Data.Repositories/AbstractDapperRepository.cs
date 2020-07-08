using AutoMapper;
using Dapper;
using ITMat.Data.DTO;
using ITMat.Data.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ITMat.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource">Type of the Entity in the database.</typeparam>
    /// <typeparam name="TDestination">Type of the outgoing DTO.</typeparam>
    internal abstract class AbstractDapperRepository<TSource, TDestination>
        where TSource : AbstractEntity
        where TDestination : AbstractDTO
    {
        private readonly string connectionString;
        private readonly IMapper mapper;

        protected AbstractDapperRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DbConnectionString");
            mapper = MapperFactory.Create<GenericProfile>();
        }

        protected AbstractDapperRepository(IConfiguration configuration, IMapper mapper)
        {
            connectionString = configuration.GetConnectionString("DbConnectionString");
            this.mapper = mapper;
        }

        protected async Task<IEnumerable<TDestination>> QueryMultipleAsync(string query, object param = null)
            => await QueryMultipleAsync<TSource, TDestination>(query, param);

        protected async Task<IEnumerable<D>> QueryMultipleAsync<S, D>(string query, object param = null)
            => await QueryAsync(async conn =>
            {
                var result = await conn.QueryAsync<S>(query, param);
                return result.Select(s => mapper.Map<D>(s));
            });


        protected async Task<IEnumerable<TDestination>> QueryMultipleAsync<TSecond>(string query, Func<TSource, TSecond, TSource> map, object param = null)
            => await QueryAsync(async conn
                => mapper.Map<IEnumerable<TDestination>>(await conn.QueryAsync(query, map, param)));

        protected async Task<TDestination> QuerySingleAsync(string query, object param = null)
            => await QueryAsync(async conn =>
            {
                var result = await conn.QuerySingleAsync<TSource>(query, param);
                return mapper.Map<TDestination>(result);
            });

        /// <summary>
        /// Retrieve a single row with multi mapping.
        /// </summary>
        /// <typeparam name="TSecond">Secondary source-type that is retrieved from the query. Used when multiple types/columns are joined.</typeparam>
        /// <param name="query">The SQL query</param>
        /// <param name="map">Function to multi map. This function maps <typeparamref name="TSecond"/> to <typeparamref name="TSource"/></param>
        /// <param name="param">The parameters to use for this query</param>
        /// <returns>A single instance of <typeparamref name="TDestination"/></returns>
        protected async Task<TDestination> QuerySingleAsync<TSecond>(string query, Func<TSource, TSecond, TSource> map, object param = null)
            => await QueryAsync(async conn =>
                mapper.Map<TDestination>((await conn.QueryAsync(query, map, param)).First()));

        protected async Task<T> QuerySingleAsync<T>(string query, object param)
            => await QueryAsync(async conn =>
                await conn.QuerySingleAsync<T>(query, param));

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

        protected class GenericProfile : Profile
        {
            public GenericProfile()
                => CreateMap<TSource, TDestination>();
        }

        protected class MapperFactory
        {
            public static IMapper Create<TProfile>() where TProfile : Profile, new()
                => new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new TProfile())));
        }
    }
}