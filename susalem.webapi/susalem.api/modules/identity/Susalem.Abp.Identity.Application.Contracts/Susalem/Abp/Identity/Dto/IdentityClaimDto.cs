using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.Identity
{
    public class IdentityClaimDto : EntityDto<Guid>
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
