using Xunit;
using FluentAssertions;
using AutoFixture.Xunit2;

namespace Blaszm.Domain.Referrals.Tests;

public class Patient_Tests
{

    [Theory]
    [AutoData]
    public void Should_Be_Able_To_Create_Patient(PatientName name)
    {
        // act
        var patient = Patient.Create(name);

        // assert
        patient.Should().NotBeNull();
        patient.Name.FirstName.Should().Be(name.FirstName);
        patient.Name.LastName.Should().Be(name.LastName);
    }

}
