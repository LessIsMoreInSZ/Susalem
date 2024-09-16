using System.Threading.Tasks;

namespace Susalem.Abp.IdentityServer.IdentityResources
{
    public interface ICustomIdentityResourceDataSeeder
    {
        Task CreateCustomResourcesAsync();
    }
}
