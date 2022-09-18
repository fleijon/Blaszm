using System.Linq;
using System;
using System.IO;

namespace Blaszm.Domain.Referrals;

public sealed class Referral : IAggregate
{
    private Referral(IEnumerable<IDomainEvent> domainEvents)
    {
        _domainEvents = domainEvents.OrderBy(de => de.Order).ToList();
        foreach(var @event in _domainEvents)
        {
            if(@event is ReferralCreated createdEvent)
            {
                Id = createdEvent.Id;
                Patient = createdEvent.Patient;
            }
            else if(@event is PatientUpdated patientUpdated)
            {
                UpdatePatient_Internal(patientUpdated);
            }
            else
            {
                throw new NotImplementedException(@event.GetType().Name);
            }
        }
    }

    private List<IDomainEvent> _domainEvents;

    public Guid Id { get; }
    public Patient Patient { get; private set; }
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;

    public static Referral Projection(IEnumerable<IDomainEvent> stream) => new Referral(stream);

    public static Referral Create(Patient patient)
    {
        var id = Guid.NewGuid();
        var createdEvent = new ReferralCreated(id, patient, 0);
        var domainEvents = new List<IDomainEvent>();
        domainEvents.Add(createdEvent);
        return new Referral(domainEvents);
    }

    public void UpdatePatient(Patient patient)
    {
        var @event = new PatientUpdated(patient, GetCurrentOrder());
        UpdatePatient_Internal(@event);
        _domainEvents.Add(@event);
    }

    private void UpdatePatient_Internal(PatientUpdated patientUpdated)
    {
        Patient = patientUpdated.Patient;
    }

    private int GetCurrentOrder() => _domainEvents.Select(de => de.Order).Max() + 1;
}

public sealed class ReferralCreated : IDomainEvent
{
    public ReferralCreated(Guid id, Patient patient, int order)
    {
        Id = id;
        Patient = patient;
        Order = order;
    }

    public Guid Id { get; }
    public Patient Patient { get; }
    public int Order { get; }
}

public sealed class PatientUpdated : IDomainEvent
{
    public PatientUpdated(Patient patient, int order)
    {
        Patient = patient;
        Order = order;
    }

    public Patient Patient { get; }
    public int Order { get; }
}
