﻿using Susalem.Abp.OpenIddict.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict.Tokens;

namespace Susalem.Abp.OpenIddict.Tokens;

[Authorize(AbpOpenIddictPermissions.Tokens.Default)]
public class OpenIddictTokenAppService : OpenIddictApplicationServiceBase, IOpenIddictTokenAppService
{
    protected IRepository<OpenIddictToken, Guid> Repository { get; }

    public OpenIddictTokenAppService(IRepository<OpenIddictToken, Guid> repository)
    {
        Repository = repository;
    }

    [Authorize(AbpOpenIddictPermissions.Tokens.Delete)]
    public async virtual Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);

        await CurrentUnitOfWork.SaveChangesAsync();
    }

    public async virtual Task<OpenIddictTokenDto> GetAsync(Guid id)
    {
        var scope = await Repository.GetAsync(id);

        return scope.ToDto();
    }

    public async virtual Task<PagedResultDto<OpenIddictTokenDto>> GetListAsync(OpenIddictTokenGetListInput input)
    {
        var queryable = await Repository.GetQueryableAsync();
        if (input.ClientId.HasValue)
        {
            queryable = queryable.Where(x => x.ApplicationId == input.ClientId);
        }
        if (input.BeginCreationTime.HasValue)
        {
            queryable = queryable.Where(x => x.CreationTime >= input.BeginCreationTime);
        }
        if (input.EndCreationTime.HasValue)
        {
            queryable = queryable.Where(x => x.CreationTime <= input.BeginCreationTime);
        }
        if (input.BeginExpirationDate.HasValue)
        {
            queryable = queryable.Where(x => x.ExpirationDate >= input.BeginCreationTime);
        }
        if (input.EndExpirationDate.HasValue)
        {
            queryable = queryable.Where(x => x.ExpirationDate <= input.BeginCreationTime);
        }
        if (!input.Status.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => x.Status == input.Status);
        }
        if (!input.Type.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => x.Type == input.Type);
        }
        if (!input.Subject.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => x.Subject == input.Subject);
        }
        if (!input.ReferenceId.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => x.ReferenceId == input.ReferenceId);
        }
        if (!input.Filter.IsNullOrWhiteSpace())
        {
            queryable = queryable.Where(x => x.Subject.Contains(input.Filter) ||
                x.Status.Contains(input.Filter) || x.Type.Contains(input.Filter) ||
                x.Payload.Contains(input.Filter) || x.Properties.Contains(input.Filter) ||
                x.ReferenceId.Contains(input.ReferenceId));
        }
        queryable = queryable
            .OrderBy(input.Sorting ?? $"{nameof(OpenIddictToken.CreationTime)} DESC")
            .PageBy(input.SkipCount, input.MaxResultCount);

        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var entites = await AsyncExecuter.ToListAsync(queryable);

        return new PagedResultDto<OpenIddictTokenDto>(totalCount,
            entites.Select(entity => entity.ToDto()).ToList());
    }
}
