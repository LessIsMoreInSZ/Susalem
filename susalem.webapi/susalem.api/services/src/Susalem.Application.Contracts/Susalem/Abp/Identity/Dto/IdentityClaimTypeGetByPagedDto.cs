using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.Identity
{
    public class IdentityClaimTypeGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
