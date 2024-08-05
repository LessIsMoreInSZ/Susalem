using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace SusalemAbp.Shared.Hosting.AspNetCore
{
    [Dependency(ReplaceServices = true)]
    public class SusalemAbpBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SusalemAbp";
    }
}
