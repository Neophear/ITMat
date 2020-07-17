using AutoMapper;
using ITMat.Core.Data.Interfaces;
using ITMat.Core.DTO;
using ITMat.Core.Interfaces;
using ITMat.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITMat.Core.Services
{
    public class ItemService : AbstractService<Item, ItemDTO>, IItemService
    {
        private readonly IItemRepository repo;

        public ItemService(IItemRepository repo)
            => this.repo = repo;

        public async Task<ItemDTO> GetItemAsync(int id)
            => Mapper.Map<ItemDTO>(await repo.GetItemAsync(id));

        public async Task<IEnumerable<ItemDTO>> GetItemsAsync()
            => Mapper.Map<IEnumerable<ItemDTO>>(await repo.GetItemsAsync());

        public async Task<IEnumerable<ItemTypeDTO>> GetItemTypesAsync()
            => Mapper.Map<IEnumerable<ItemTypeDTO>>(await repo.GetItemTypesAsync());

        public async Task<IEnumerable<string>> GetModelsAsync()
            => await repo.GetModelsAsync();

        public async Task<int> InsertItemAsync(ItemDTO item)
        {
            Validate(item);
            return await repo.InsertItemAsync(Mapper.Map<Item>(item));
        }

        public async Task UpdateItemAsync(int id, ItemDTO item)
        {
            await repo.UpdateItemAsync(id, Mapper.Map<Item>(item));
        }

        private void Validate(ItemDTO item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Identifier is null)
                throw new ArgumentNullException(nameof(item.Identifier));

            if (String.IsNullOrEmpty(item.Identifier) || item.Identifier.Length > 250)
                throw new ArgumentException($"{nameof(item.Identifier)} can not be empty or longer than 250 characters.");

            if (String.IsNullOrEmpty(item.Model) || item.Model.Length > 250)
                throw new ArgumentException($"{nameof(item.Model)} can not be empty or longer than 250 characters.");

            if (item.Type is null)
                throw new ArgumentNullException(nameof(item.Type));
        }

        private class ItemProfile : Profile
        {
            public ItemProfile()
            {
                CreateMap<Item, ItemDTO>();
                CreateMap<ItemType, ItemTypeDTO>();
            }
        }
    }
}