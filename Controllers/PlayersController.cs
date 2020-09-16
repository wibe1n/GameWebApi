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
        public Task<Player> Get(Guid id){
            return repository.Get(id);
        }

        /*[HttpGet]
        public ActionResult<string> TestCall(){
            return "Test complete";
        }*/

        [HttpGet]
        public Task<Player[]> GetAll(){
            return repository.GetAll();

        }

        [HttpPost]
        public Task<Player> Create(NewPlayer player){
            Player createdPlayer = new Player();
            createdPlayer.Name = player.Name;
            createdPlayer.Id = Guid.NewGuid();
            createdPlayer.IsBanned = false;
            createdPlayer.Level = 1;
            createdPlayer.Score = 0;
            createdPlayer.CreationTime = DateTime.UtcNow;
            return repository.Create(createdPlayer);
        }
        [HttpPut("{id}")]
        public Task<Player> Modify(Guid id, ModifiedPlayer player){
                return repository.Modify(id, player);
        }
        [HttpDelete("{id:guid}")]
        public Task<Player> Delete(Guid id){
            return repository.Delete(id);
        }

    }


}