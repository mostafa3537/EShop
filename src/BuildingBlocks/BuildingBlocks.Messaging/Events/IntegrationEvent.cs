namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    public Guid Id { get; set; }
    public DateTime OccuredOn { get; set; }
    public string? EventType => GetType().AssemblyQualifiedName;
}