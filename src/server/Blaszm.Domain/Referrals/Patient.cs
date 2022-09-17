namespace Blaszm.Domain.Referrals;

public sealed class Patient
{
    private Patient(Guid id, PatientName name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public PatientName Name { get; }

    public static Patient Create(PatientName name)
    {
        var id = Guid.NewGuid();
        return new Patient(id, name);
    }
}
