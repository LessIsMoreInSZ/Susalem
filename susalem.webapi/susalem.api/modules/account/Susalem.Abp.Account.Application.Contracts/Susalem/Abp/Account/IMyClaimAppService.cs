using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Susalem.Abp.Account
{
    public interface IMyClaimAppService : IApplicationService
    {
        Task ChangeAvatarAsync(ChangeAvatarInput input);
    }
}
