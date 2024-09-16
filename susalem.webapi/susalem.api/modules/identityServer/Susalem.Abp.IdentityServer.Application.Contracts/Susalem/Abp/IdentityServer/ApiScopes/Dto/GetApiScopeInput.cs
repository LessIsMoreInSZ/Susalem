using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.IdentityServer.ApiScopes
{
    public class GetApiScopeInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
