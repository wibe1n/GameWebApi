using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GameWebApi{

    class MongoDbRepository : IRepository
    {
        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

        public MongoDbRepository(){
            var mongoClient = new MongoClient("mongodb://localhost:20017");
            var dataBase = mongoClient.GetDatabase("game");
            _playerCollection = dataBase.GetCollection<Player>("players");
            _bsonDocumentCollection = dataBase.GetCollection<BsonDocument>("players");
        }
        public async Task<Player> Create(Player player)
        {
            await _playerCollection.InsertOneAsync(player);
            return player;
        }

        public async Task<Item> CreateItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null){
                throw new NotFoundException("player not found.");
            }
            if(player.items == null){
                player.items = new List<Item>();
            }
            player.items.Add(item);
            await _playerCollection.ReplaceOneAsync(filter, player);
            return item;
        }

        public async Task<Player> Delete(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            return await _playerCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task<Item> DeleteItem(Guid playerId, Item item)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null){
                throw new NotFoundException("player not found.");
            }
            if(player.items == null){
                throw new NotFoundException("player doesnt have any items");
            }
            foreach (var _item in player.items)
            {
                if(_item.itemId == item.itemId){
                    player.items.Remove(item);
                    await _playerCollection.ReplaceOneAsync(filter, player);
                    return item;
                }
            }
            throw new NotFoundException("player doesn't have this item");           
        }

        public Task<Player> Get(Guid id)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            return _playerCollection.Find(filter).FirstAsync();
        }

        public async Task<Player[]> GetAll()
        {
            var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            return players.ToArray();
        }

        public async Task<Item[]> GetAllItems(Guid playerId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null){
                throw new NotFoundException("player not found");
            }
            if (player.items == null){
                throw new NotFoundException("player doesnt have any items");
            }
            return player.items.ToArray();
        }

        public async Task<Item> GetItem(Guid playerId, Guid itemId)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null) {
                throw new NotFoundException("player not found");
            }
            if (player.items == null){
                throw new NotFoundException("player doesnt have any items");
            }
            foreach (var item in player.items)
            {
                if(item.itemId == itemId){
                    return item;
                }
            }
            throw new NotFoundException("player doesnt have this item");
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
            Player _player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null) {
                throw new NotFoundException("player not found");
            }
            _player.Score = player.Score;
            await _playerCollection.FindOneAndReplaceAsync(filter, _player);
            return _player;
        }

        public async Task<Item> UpdateItem(Guid playerId, Item item)
        {
         var filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
            Player player = await _playerCollection.Find(filter).FirstAsync();
            if (player == null) {
                throw new NotFoundException("player not found");
            }
            if (player.items == null){
                throw new NotFoundException("player doesnt have any items");
            }
            foreach (var _item in player.items)
            {
                if(_item.itemId == item.itemId){
                    _item.Level = item.Level;
                    await _playerCollection.FindOneAndReplaceAsync(filter, player);
                    return _item;
                }
            }
            throw new NotFoundException("player doesnt have this item");   
        }
    }
}