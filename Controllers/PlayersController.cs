using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace GameWebApi.Controllers {

    [ApiController]
    [Route("[controller]")]
    public class PlayersController : Controller {
        private readonly IRepository repository;
        public PlayersController(IRepository repo){
            repository = repo;
        }

        [HttpGet("{id:Guid}")]
        public async Task<Player> Get(Guid id){
            return await repository.Get(id);
        }

        /*[HttpGet]
        public ActionResult<string> TestCall(){
            return "Test complete";
        }*/

        [HttpGet]
        public async Task<Player[]> GetAll(){
            return await repository.GetAll();

        }

        [HttpPost]
        public async Task<Player> Create(NewPlayer player){
            Player createdPlayer = new Player();
            createdPlayer.Name = player.Name;
            createdPlayer.Id = Guid.NewGuid();
            createdPlayer.IsBanned = false;
            createdPlayer.Level = 1;
            createdPlayer.Score = 0;
            createdPlayer.CreationTime = DateTime.UtcNow;
            return await repository.Create(createdPlayer);
        }
        [HttpPut("{id}")]
        public async Task<Player> Modify(Guid id, ModifiedPlayer player){
                return await repository.Modify(id, player);
        }
        [HttpDelete("{id:guid}")]
        public async Task<Player> Delete(Guid id){
            return await repository.Delete(id);
        }

    }


}