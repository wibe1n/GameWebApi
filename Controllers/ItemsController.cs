using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace GameWebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class ItemsController : Controller {
        private readonly IRepository repository;
        public ItemsController(IRepository repo){
            repository = repo;
        }

        [HttpPost("/players/{playerId}/items")]
        public async Task<Item> CreateItem(Guid playerId, NewItem item)
        {
        Player player = await repository.Get(playerId);
            if (player != null){
                Item _item = new Item();
                _item.Level = item.lvl;
                _item.Type = item.Type;
                _item.CreationDate = DateTime.UtcNow;
                ItemCheck(item.Type, player.Level);
                return await repository.CreateItem(playerId, _item);
            } 
            return null;
        }

        [HttpDelete("/players/{playerId}/items")]
        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            return await repository.DeleteItem(playerId, item);
        }

        [HttpGet("/players/{playerId}/items")]
        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            return await repository.GetAllItems(playerId);
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            return await repository.GetItem(playerId, itemId);
        }
        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
            return await repository.UpdateItem(playerId, item);
        }
        public bool ItemCheck(string itemType, int playerLevel){
            if(itemType == validItemType.sword.ToString() && playerLevel < 3){
                throw new InvalidItemException("Swords for players below level 3 are not allowed");
            }
            return true;
        }


    }


}