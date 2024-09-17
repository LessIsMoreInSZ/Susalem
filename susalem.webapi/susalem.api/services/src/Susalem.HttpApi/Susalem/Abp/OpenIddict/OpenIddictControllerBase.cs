using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.OpenIddict.Localization;

namespace Susalem.Abp.OpenIddict;

public abstract class OpenIddictControllerBase : AbpControllerBase
{
    protected OpenIddictControllerBase()
    {
        LocalizationResource = typeof(AbpOpenIddictResource);
    }
}
