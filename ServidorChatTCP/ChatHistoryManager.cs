using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChatHistoryManager
{
    private readonly IMongoCollection<BsonDocument> _chatCollection;

    public ChatHistoryManager(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _chatCollection = database.GetCollection<BsonDocument>(collectionName);
    }

    // Método para guardar un mensaje en el historial de chat
    public async Task SaveMessageAsync(string user, string message, DateTime timestamp)
    {
        var document = new BsonDocument
        {
            { "user", user },
            { "message", message },
            { "timestamp", timestamp }
        };

        await _chatCollection.InsertOneAsync(document);
    }

    // Método para obtener el historial completo de mensajes
    public async Task<List<BsonDocument>> GetChatHistoryAsync()
    {
        return await _chatCollection.Find(new BsonDocument()).ToListAsync();
    }
}
