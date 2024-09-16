using System;
using Volo.Abp.Application.Services;

namespace Susalem.Abp.IdentityServer.ApiScopes
{
    public interface IApiScopeAppService : 
        ICrudAppService<
            ApiScopeDto,
            Guid,
            GetApiScopeInput,
            ApiScopeCreateDto,
            ApiScopeUpdateDto>
    {
    }
}
