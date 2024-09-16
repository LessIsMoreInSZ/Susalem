using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.IdentityServer.ApiResources
{
    public class ApiResourceGetByPagedInputDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
