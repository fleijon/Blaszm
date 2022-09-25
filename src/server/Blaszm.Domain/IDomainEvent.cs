namespace Blaszm.Domain;

public interface IDomainEvent
{
    public int Version { get; }
}
