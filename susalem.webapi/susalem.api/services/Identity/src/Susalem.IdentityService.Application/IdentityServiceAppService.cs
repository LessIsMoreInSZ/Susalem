using System;
using System.Collections.Generic;
using System.Text;
using Susalem.IdentityService.Localization;
using Volo.Abp.Application.Services;

namespace Susalem.IdentityService;

/* Inherit your application services from this class.
 */
public abstract class IdentityServiceAppService : ApplicationService
{
    protected IdentityServiceAppService()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
