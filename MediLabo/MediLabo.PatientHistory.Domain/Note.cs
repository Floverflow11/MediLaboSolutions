using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MediLabo.PatientHistory.Domain;

public record Note
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public int PatientId { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}