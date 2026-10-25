using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Game.Domain
{
    public class GameTurnEntity{
        
        [BsonElement]
        public IReadOnlyList<Player> Players;
        
        [BsonElement]
        public Guid WinnerId;

        [BsonElement]
        public int TurnIndex;
        
        [BsonElement]
        public Guid GameId;

        
        [BsonConstructor] 
        public GameTurnEntity(IReadOnlyList<Player> players, int turnIndex, Guid gameId)
        {
            this.Players = players;
            this.TurnIndex = turnIndex;
            this.GameId = gameId;
        }
    }
}