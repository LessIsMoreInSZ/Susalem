using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.IdentityServer.IdentityResources
{
    public class IdentityResourceGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
