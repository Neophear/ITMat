using ITMat.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Data.Interfaces
{
    interface IItemRepository
    {
        Task<IEnumerable<ItemDTO>> GetItemsAsync();
        Task<ItemDTO> GetItemAsync(int id);
        Task<int> InsertItemAsync(ItemDTO item);
        Task UpdateItemAsync(int id, ItemDTO item);
    }
}