using Blaszm.Domain.Referrals.Events;

namespace Blaszm.Domain.Referrals;

public sealed class Referral : IAggregate
{
    private Referral(IEnumerable<IDomainEvent> domainEvents)
    {
        _domainEvents = domainEvents.OrderBy(de => de.Version).ToList();
        foreach (var @event in _domainEvents)
        {
            switch (@event)
            {
                case v1.ReferralCreated createdEvent:
                    Id = createdEvent.Id;
                    Patient = createdEvent.Patient;
                    break;
                case v1.PatientUpdated patientUpdated:
                    UpdatePatient_Internal(patientUpdated);
                    break;
                default:
                    throw new NotImplementedException(@event.GetType().Name);
            }
        }

        if (Patient is null)
            throw new InvalidOperationException("Patient info could not be found.");
    }

    private readonly List<IDomainEvent> _domainEvents;
    public Guid Id { get; }
    public Patient Patient { get; private set; }
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;
    public static Referral Projection(IEnumerable<IDomainEvent> stream) => new(stream);

    public static Referral Create(Patient patient)
    {
        var id = Guid.NewGuid();
        var createdEvent = new v1.ReferralCreated(id, patient, 0);
        var domainEvents = new List<IDomainEvent>
        {
            createdEvent
        };

        return new Referral(domainEvents);
    }

    public void UpdatePatient(Patient patient)
    {
        var @event = new v1.PatientUpdated(patient, GetCurrentVersion() + 1);
        UpdatePatient_Internal(@event);
        _domainEvents.Add(@event);
    }

    private void UpdatePatient_Internal(v1.PatientUpdated patientUpdated) => Patient = patientUpdated.Patient;

    private int GetCurrentVersion() => _domainEvents.Max(de => de.Version);
}
