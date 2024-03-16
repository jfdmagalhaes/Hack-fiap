using Domain.Interfaces;
using System.Text.Json.Serialization;

namespace Domain.Aggregates;
public class Midia : IAggregateRoot
{    public DateTime CreationDate { get; set; }
    public string FilePath { get; set; }

    [JsonIgnore]
    public int Id { get; set; }
}