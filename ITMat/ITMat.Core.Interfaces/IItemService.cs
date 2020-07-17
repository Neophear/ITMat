using ITMat.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetItemsAsync();
        Task<ItemDTO> GetItemAsync(int id);
        Task<int> InsertItemAsync(ItemDTO item);
        Task UpdateItemAsync(int id, ItemDTO item);

        Task<IEnumerable<string>> GetModelsAsync();
        Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync();
    }
}