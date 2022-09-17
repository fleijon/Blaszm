namespace Blaszm.Domain.Referrals;

public sealed class Referral
{
    private Referral(Guid id, Patient patient)
    {
        Id = id;
        Patient = patient;
    }

    public Guid Id { get; }
    public Patient Patient { get; }

    public static Referral Create(Patient patient)
    {
        var id = Guid.NewGuid();
        return new Referral(id, patient);
    }
}
