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
    internal abstract class AbstractDapperRepository<TSource, TDestination>
        where TSource : AbstractEntity
        where TDestination : AbstractDTO
    {
        private readonly string connectionString;
        private readonly IMapper mapper;

        protected AbstractDapperRepository(IConfiguration configuration)
        {
            connectionString= configuration.GetConnectionString("DbConnectionString");
            mapper = MapperFactory.Create<GenericProfile>();
        }

        protected async Task<IEnumerable<TDestination>> QueryMultipleAsync(string query, object param = null)
            => await QueryAsync(async conn =>
            {
                var result = await conn.QueryAsync<TSource>(query, param);
                return result.Select(source => mapper.Map<TDestination>(source));
            });

        protected async Task<TDestination> QuerySingleAsync(string query, object param = null)
            => await QueryAsync(async conn =>
            {
                var result = await conn.QuerySingleAsync<TSource>(query, param);
                return mapper.Map<TDestination>(result);
            });

        protected async Task<T> QueryAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                return await func(connection);
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("Object was not found.");
            }
            catch (Exception e)
            {
                throw new DataException("Something went wrong.", e);
            }
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