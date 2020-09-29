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
        if (Object == null){
            Blueprint newprint = new Blueprint();
            newprint.players.Add(player);
            File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(newprint));
            return null;
        }
        else{
        Object.players.Add(player);
        File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
        //purpose of returning???
        return null;
        }
    }
    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var _player in Object.players)
        {
            if(_player.Id == id){
                _player.Score = player.Score;
                File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
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
                File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
                return null;
            }
        }
        throw new NotFoundException("Player with id " +id+ " not found.");
    }

    public Task<Item> CreateItem(Guid playerId, Item item)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        
        foreach (var player in Object.players)
        {
            if(player.Id == playerId){
                if(player.items == null){
                    player.items = new List<Item>();
                    player.items.Add(item);
                    File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
                    return null;
                }
                else{
                    player.items.Add(item);
                    File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
                    return null;
                }
            }         
        }   
        
        
        throw new NotFoundException("Player with id" +playerId+ " not found");      
    }

    public Task<Item> DeleteItem(Guid playerId, Item item)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        
        foreach (var player in Object.players)
        {
            if(player.Id == playerId)
            {
                if(player.items == null)
                {
                    throw new NotFoundException("player doesn't have any items.");    
                }
                else
                {
                    foreach (var _item in player.items)
                    {
                        if(_item.itemId == item.itemId)
                        {
                            player.items.Remove(item);
                            File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
                            return null;
                        }
                    }
                    throw new NotFoundException("player doesn't have that item.");
                    
                }
            }         
        }   
        throw new NotFoundException("Player with id" +playerId+ " not found");
    }
    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var player in Object.players)
        {
            if (player.Id == playerId){
                return player.items.ToArray();
            }
        }
        throw new NotFoundException("Player with id" +playerId+ " not found");
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var player in Object.players)
        {
            if (player.Id == playerId){
                foreach (var item in player.items)
                {
                    if (item.itemId == itemId){
                        return item;
                    }
                }
                throw new NotFoundException("Player doesn't have that item.");
            }
        }
        throw new NotFoundException("Player with id" +playerId+ " not found");
    }
    public Task<Item> UpdateItem(Guid playerId, Item item)
    {
        Blueprint Object = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev"), typeof(Blueprint)) as Blueprint;
        foreach (var player in Object.players)
        {
            if (player.Id == playerId){
                foreach (var _item in player.items)
                {
                    if (_item.itemId == item.itemId){
                        _item.Level = item.Level;
                        File.WriteAllText(@"C:\Users\Ville\Documents\GameWebApi\game-dev", JsonConvert.SerializeObject(Object));
                    }
                }
                throw new NotFoundException("Player doesn't have that item.");
            }
        }
        throw new NotFoundException("Player with id" +playerId+ " not found");
    }


    public string ReadAllText(string path) {
        string file = File.ReadAllText(path);
        return file;

    }





}
}