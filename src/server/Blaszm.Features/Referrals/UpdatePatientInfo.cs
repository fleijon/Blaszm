using Blaszm.Domain.Referrals;
using MediatR;

namespace Blaszm.Features.Referrals;

public static class UpdatePatientInfo
{
    public sealed record ReferralId(Guid Value);
    public sealed record PatientInfo(string FirstName, string LastName);
    public sealed record Request(ReferralId Id, PatientInfo NewPatientInfo) : IRequest;

    public sealed class Handler : IRequestHandler<Request>
    {
        private readonly IReferralRepository _repo;
        public Handler(IReferralRepository repo) => _repo = repo;

        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        {
            var referral = await _repo.GetById(request.Id.Value);
            referral.UpdatePatient(
                new Patient(
                    new PatientName(
                            request.NewPatientInfo.FirstName,
                            request.NewPatientInfo.LastName)));

            return Unit.Value;
        }
    }
}
