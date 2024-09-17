using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.Identity
{
    public class OrganizationUnitGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
