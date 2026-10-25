using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoGameTurnRepository : IGameTurnRepository
    {
        private readonly IMongoCollection<GameTurnEntity> gameTurnCollection;
        public const string CollectionName = "game_turns";
        
        public MongoGameTurnRepository(IMongoDatabase database)
        {
            gameTurnCollection = database.GetCollection<GameTurnEntity>(CollectionName);
            var indexModel =
                new CreateIndexModel<GameTurnEntity>(Builders<GameTurnEntity>.IndexKeys
                    .Ascending(x => x.GameId)
                    .Ascending(x => x.TurnIndex), new CreateIndexOptions { Unique = true }
                );
            gameTurnCollection.Indexes.CreateOne(indexModel);
        }
        public GameTurnEntity Insert(GameTurnEntity gameTurn)
        {
            gameTurnCollection.InsertOne(gameTurn);
            return gameTurn;
        }
        
        public List<GameTurnEntity> FindAllGamesById(Guid gameId, int count)
        {
            return gameTurnCollection
                .Find(x => x.GameId == gameId)
                .SortByDescending(x => x.TurnIndex)
                .Limit(count).
                ToList();
        }
    }
}