namespace Blaszm.Domain.Referrals.Events;

public static class v1
{
    public sealed class ReferralCreated : IDomainEvent
    {
        public ReferralCreated(Guid id, Patient patient, int version)
        {
            Id = id;
            Patient = patient;
            Version = version;
        }

        public Guid Id { get; }
        public Patient Patient { get; }
        public int Version { get; }
    }

    public sealed class PatientUpdated : IDomainEvent
    {
        public PatientUpdated(Patient patient, int version)
        {
            Patient = patient;
            Version = version;
        }

        public Patient Patient { get; }
        public int Version { get; }
    }

}
