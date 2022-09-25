namespace Blaszm.Domain.Referrals;

public sealed class Patient
{
    public Patient(PatientName name, BirthDate birthDate)
    {
        Name = name;
        BirthDate = birthDate;
    }
    public PatientName Name { get; }
    public BirthDate BirthDate { get; }
}
