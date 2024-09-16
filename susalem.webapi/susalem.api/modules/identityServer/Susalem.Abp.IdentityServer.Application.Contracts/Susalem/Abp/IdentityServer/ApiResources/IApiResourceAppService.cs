using System;
using Volo.Abp.Application.Services;

namespace Susalem.Abp.IdentityServer.ApiResources
{
    public interface IApiResourceAppService : 
        ICrudAppService<
            ApiResourceDto,
            Guid,
            ApiResourceGetByPagedInputDto,
            ApiResourceCreateDto,
            ApiResourceUpdateDto>
    {
    }
}
