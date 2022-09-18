namespace Blaszm.Domain;

public interface IAggregate
{
    public IEnumerable<IDomainEvent> DomainEvents { get; }
}
