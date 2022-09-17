namespace Blaszm.Domain.Referrals;

public sealed class PatientName
{

    public PatientName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }
}
