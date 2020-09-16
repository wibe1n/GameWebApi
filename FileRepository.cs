using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GameWebApi{

    public class FileRepository : IRepository{
    public async Task<Player> Get(Guid id){
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var player in Object.players)
        {
            if(player.Id == id){
                return player;
            }
        }
        throw new NotFoundException("Player with id " +id+ " not found.");
        }
    public async Task<Player[]> GetAll()
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        return Object.players.ToArray();
    }
    public async Task<Player> Create(Player player)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        Object.players.Add(player);
        File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
        //purpose of returning???
        return player;
    }
    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var _player in Object.players)
        {
            if(_player.Id == id){
                _player.Score = player.Score;
                return _player;
            }
        }
        throw new NotFoundException("Player with id " +id+ " not found.");

    }
    public async Task<Player> Delete(Guid id)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var player in Object.players)
        {
            if(player.Id == id){
                Object.players.Remove(player);
            }
        }
        throw new NotFoundException("Player with id " +id+ " not found.");
    }


    public string ReadAllText(string path) {
        string file = File.ReadAllText(path);
        return file;

    }





}
}