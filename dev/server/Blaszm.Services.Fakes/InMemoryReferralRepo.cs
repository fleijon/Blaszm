using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using Blaszm.Domain.Referrals;
using Blaszm.Domain;

namespace Blaszm.Services.Fakes;

public class InMemoryReferralRepo : IReferralRepository
{
    private readonly Dictionary<Guid, List<IDomainEvent>> _store = new();
    public Task<Referral> GetById(Guid id)
    {
        if(!_store.ContainsKey(id))
            throw new InvalidOperationException("Referral with given id does not exist.");

        return Task.FromResult(Referral.Projection(_store[id]));
    }

    public Task Store(Referral referral)
    {
        // TODO: Add lock

        if(_store.ContainsKey(referral.Id))
            // TODO: Update the event list instead
            _store.Remove(referral.Id);

        _store.Add(referral.Id, referral.DomainEvents.ToList());

        return Task.CompletedTask;
    }
}
