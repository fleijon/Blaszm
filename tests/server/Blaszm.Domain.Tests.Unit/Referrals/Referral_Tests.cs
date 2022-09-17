using Xunit;
using FluentAssertions;
using AutoFixture.Xunit2;

namespace Blaszm.Domain.Referrals.Tests;

public class Referral_Tests
{

    [Theory]
    [AutoData]
    public void Should_Be_Able_To_Create_Referral(PatientName name)
    {
        // arrange
        var patient = Patient.Create(name);

        // act
        var referral = Referral.Create(patient);

        // assert
        referral.Should().NotBeNull();
        referral.Patient.Id.Should().Be(patient.Id);
    }
}
