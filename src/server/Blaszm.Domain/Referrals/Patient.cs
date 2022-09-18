namespace Blaszm.Domain.Referrals;

public sealed class Patient
{
    public Patient(PatientName name)
    {
        Name = name;
    }

    public PatientName Name { get; }

}
