using ITMat.Core.Data.Interfaces;
using ITMat.Core.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Repositories
{
    public class ItemRepository : AbstractRepository<Item>, IItemRepository
    {
        #region Queries
        private const string SqlGetItems = "select * from item i inner join itemtype t on i.type_id = t.id",
                             SqlGetItem = SqlGetItems + " where i.id = @id",
                             SqlGetActiveItems = SqlGetItems + " where discarded = 0",
                             SqlInsertItem = "insert into item (identifier, model, type_id) values (@identifier, @model, @typeId)",
                             SqlUpdateItem = "update item set identifier = @identifier, model = @model, type_id = @typeId where id = @id",
                             SqlGetModels = "select distinct model from item",
                             SqlGetItemTypes = "select * from itemtype";
        #endregion

        public ItemRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<Item> GetItemAsync(int id)
            => await QuerySingleAsync(SqlGetItem, new { id });

        public async Task<IEnumerable<Item>> GetItemsAsync()
            => await QueryMultipleAsync(SqlGetItems);

        public async Task<IEnumerable<Item>> GetActiveItemsAsync()
            => await QueryMultipleAsync(SqlGetActiveItems);

        public async Task<int> InsertItemAsync(Item item)
            => await QuerySingleAsync<int>(SqlInsertItem, new { item.Identifier, item.Model, typeId = item.Type.Id });

        public async Task UpdateItemAsync(int id, Item item)
        {
            var rowsAffected = await ExecuteAsync(SqlUpdateItem, new { id, item.Identifier, item.Model, typeId = item.Type.Id });

            if (rowsAffected != 1)
                throw new KeyNotFoundException($"Could not find item with id {id}");
        }

        public async Task<IEnumerable<string>> GetModelsAsync()
            => await QueryMultipleAsync<string>(SqlGetModels);

        public async Task<IEnumerable<ItemType>> GetItemTypesAsync()
            => await QueryMultipleAsync<ItemType>(SqlGetItemTypes);
    }
}