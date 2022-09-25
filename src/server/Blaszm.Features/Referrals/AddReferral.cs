using MediatR;
using Blaszm.Domain.Referrals;

namespace Blaszm.Features.Referrals;

public static class AddReferral
{
    public sealed record ReferralId(Guid Id);
    public sealed record PatientInfo(string FirstName, string LastName);
    public sealed record Request(PatientInfo PatientInfo) : IRequest<ReferralId>;
    public sealed class Handler : IRequestHandler<Request, ReferralId>
    {
        private readonly IReferralRepository _repo;

        public Handler(IReferralRepository repo) => _repo = repo;

        public async Task<ReferralId> Handle(Request request, CancellationToken cancellationToken)
        {
            var referral = Referral.Create(
                new Patient(
                    new PatientName(
                        request.PatientInfo.FirstName,
                        request.PatientInfo.LastName)));

            await _repo.Store(referral);

            return new(referral.Id);
        }
    }
}
