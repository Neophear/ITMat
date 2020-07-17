using ITMat.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Data.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> GetItemAsync(int id);
        Task<int> InsertItemAsync(Item item);
        Task UpdateItemAsync(int id, Item item);

        Task<IEnumerable<string>> GetModelsAsync();
        Task<IEnumerable<ItemType>> GetItemTypesAsync();
    }
}