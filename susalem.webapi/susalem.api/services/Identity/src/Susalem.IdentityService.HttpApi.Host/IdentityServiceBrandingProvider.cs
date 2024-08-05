using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Susalem.IdentityService;

[Dependency(ReplaceServices = true)]
public class IdentityServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IdentityService";
}
