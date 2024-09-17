using System;
using Volo.Abp.Application.Services;

namespace Susalem.Abp.OpenIddict.Applications;

public interface IOpenIddictApplicationAppService : 
    ICrudAppService<
        OpenIddictApplicationDto,
        Guid,
        OpenIddictApplicationGetListInput,
        OpenIddictApplicationCreateDto,
        OpenIddictApplicationUpdateDto>
{
}
