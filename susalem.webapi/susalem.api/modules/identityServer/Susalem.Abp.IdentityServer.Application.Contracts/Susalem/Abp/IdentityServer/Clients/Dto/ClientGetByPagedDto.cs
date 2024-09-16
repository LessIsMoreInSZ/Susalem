using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.IdentityServer.Clients
{
    public class ClientGetByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
