﻿using IdentityServer4.Validation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Susalem.Abp.IdentityServer.LinkUser;

public class LinkUserGrantValidator : IExtensionGrantValidator
{
    public const string ExtensionGrantType = "link_user";

    public string GrantType => ExtensionGrantType;

    protected ITokenValidator TokenValidator { get; }
    protected IdentityLinkUserManager IdentityLinkUserManager { get; }
    protected ICurrentTenant CurrentTenant { get; }
    protected ICurrentUser CurrentUser { get; }
    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }
    protected IdentityUserManager UserManager { get; }
    protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }
    protected ILogger<LinkUserGrantValidator> Logger { get; }
    protected IStringLocalizer<AbpIdentityServerResource> Localizer { get; }

    public LinkUserGrantValidator(
        ITokenValidator tokenValidator,
        IdentityLinkUserManager identityLinkUserManager,
        ICurrentTenant currentTenant,
        ICurrentUser currentUser,
        IdentityUserManager userManager,
        ICurrentPrincipalAccessor currentPrincipalAccessor,
        IdentitySecurityLogManager identitySecurityLogManager,
        ILogger<LinkUserGrantValidator> logger,
        IStringLocalizer<AbpIdentityServerResource> localizer)
    {
        TokenValidator = tokenValidator;
        IdentityLinkUserManager = identityLinkUserManager;
        CurrentTenant = currentTenant;
        CurrentUser = currentUser;
        UserManager = userManager;
        CurrentPrincipalAccessor = currentPrincipalAccessor;
        IdentitySecurityLogManager = identitySecurityLogManager;
        Logger = logger;
        Localizer = localizer;
    }

    [UnitOfWork]
    public async virtual Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var accessToken = context.Request.Raw["access_token"];
        if (accessToken.IsNullOrWhiteSpace())
        {
            context.Result = new GrantValidationResult
            {
                IsError = true,
                Error = Localizer["InvalidAccessToken"]
            };
            return;
        }

        var result = await TokenValidator.ValidateAccessTokenAsync(accessToken);
        if (result.IsError)
        {
            context.Result = new GrantValidationResult
            {
                IsError = true,
                Error = result.Error,
                ErrorDescription = result.ErrorDescription
            };
            return;
        }

        using (CurrentPrincipalAccessor.Change(result.Claims))
        {
            if (!Guid.TryParse(context.Request.Raw["LinkUserId"], out var linkUserId))
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = Localizer["InvalidLinkUserId"]
                };
                return;
            }

            Guid? linkTenantId = null;
            if (!context.Request.Raw["LinkTenantId"].IsNullOrWhiteSpace())
            {
                if (!Guid.TryParse(context.Request.Raw["LinkTenantId"], out var parsedGuid))
                {
                    context.Result = new GrantValidationResult
                    {
                        IsError = true,
                        Error = Localizer["InvalidLinkTenantId"]
                    };
                    return;
                }

                linkTenantId = parsedGuid;
            }

            var isLinked = await IdentityLinkUserManager.IsLinkedAsync(
                new IdentityLinkUserInfo(CurrentUser.GetId(), CurrentTenant.Id),
                new IdentityLinkUserInfo(linkUserId, linkTenantId));

            if (isLinked)
            {
                using (CurrentTenant.Change(linkTenantId))
                {
                    var user = await UserManager.GetByIdAsync(linkUserId);
                    var sub = await UserManager.GetUserIdAsync(user);

                    var additionalClaims = new List<Claim>();
                    await AddCustomClaimsAsync(additionalClaims, user, context);

                    context.Result = new GrantValidationResult(
                        sub,
                        GrantType,
                        additionalClaims.ToArray()
                    );
                }
            }
            else
            {
                context.Result = new GrantValidationResult
                {
                    IsError = true,
                    Error = Localizer["TheTargetUserIsNotLinkedToYou"]
                };
            }
        }
    }

    protected virtual Task AddCustomClaimsAsync(List<Claim> customClaims, IdentityUser user, ExtensionGrantValidationContext context)
    {
        if (user.TenantId.HasValue)
        {
            customClaims.Add(new Claim(AbpClaimTypes.TenantId, user.TenantId?.ToString()));
        }

        return Task.CompletedTask;
    }
}
