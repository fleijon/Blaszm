namespace Blaszm.Domain.Referrals;

public interface IReferralRepository
{
    public Task<Referral> GetById(Guid id);
    public Task Store(Referral referral);
}
