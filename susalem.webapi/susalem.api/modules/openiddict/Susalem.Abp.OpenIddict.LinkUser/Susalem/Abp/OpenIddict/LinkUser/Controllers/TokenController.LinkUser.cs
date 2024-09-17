﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Controllers;
using Volo.Abp.OpenIddict.ExtensionGrantTypes;
using Volo.Abp.Security.Claims;

namespace Susalem.Abp.OpenIddict.LinkUser.Controllers;

[IgnoreAntiforgeryToken]
[ApiExplorerSettings(IgnoreApi = true)]
public class LinkUserTokenController : AbpOpenIdDictControllerBase, ITokenExtensionGrant
{
    public string Name => LinkUserTokenExtensionGrantConsts.GrantType;

    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor => LazyServiceProvider.LazyGetRequiredService<ICurrentPrincipalAccessor>();
    protected IdentityLinkUserManager IdentityLinkUserManager => LazyServiceProvider.LazyGetRequiredService<IdentityLinkUserManager>();
    protected IdentitySecurityLogManager IdentitySecurityLogManager => LazyServiceProvider.LazyGetRequiredService<IdentitySecurityLogManager>();

    public async virtual Task<IActionResult> HandleAsync(ExtensionGrantContext context)
    {
        LazyServiceProvider = context.HttpContext.RequestServices.GetRequiredService<IAbpLazyServiceProvider>();

        var accessTokenParam = context.Request.GetParameter("access_token");
        var accessToken = accessTokenParam.ToString();
        if (accessToken.IsNullOrWhiteSpace())
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = L["InvalidAccessToken"]
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        await foreach (var result in TokenManager.ValidateAsync(accessToken))
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = result.ErrorMessage
            });

            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        var userId = await TokenManager.GetIdAsync(accessToken);
        var user = await UserManager.FindByIdAsync(userId);
        var principal = await SignInManager.CreateUserPrincipalAsync(user);

        principal.SetScopes(context.Request.GetScopes());
        principal.SetResources(await GetResourcesAsync(context.Request.GetScopes()));

        await SetClaimsDestinationsAsync(principal);

        using (CurrentPrincipalAccessor.Change(principal))
        {
            var linkUserIdParam = context.Request.GetParameter("LinkUserId");
            if (!linkUserIdParam.HasValue || !Guid.TryParse(linkUserIdParam.Value.ToString(), out var linkUserId))
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = L["InvalidLinkUserId"]
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            Guid? linkTenantId = null;
            var linkTenantIdParam = context.Request.GetParameter("LinkTenantId");
            if (linkTenantIdParam.HasValue)
            {
                if (!Guid.TryParse(linkTenantIdParam.Value.ToString(), out var parsedGuid))
                {
                    var properties = new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = L["InvalidLinkTenantId"]
                    });

                    return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                linkTenantId = parsedGuid;
            }

            var isLinked = await IdentityLinkUserManager.IsLinkedAsync(
                new IdentityLinkUserInfo(user.Id, CurrentTenant.Id),
                new IdentityLinkUserInfo(linkUserId, linkTenantId));

            if (isLinked)
            {
                using (CurrentTenant.Change(linkTenantId))
                {
                    var linkUser = await UserManager.GetByIdAsync(linkUserId);

                    return await SetSuccessResultAsync(context, linkUser);
                }
            }
            else
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIddictServerAspNetCoreConstants.Properties.Error] = OpenIddictConstants.Errors.InvalidGrant,
                    [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = L["TheTargetUserIsNotLinkedToYou"]
                });

                return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
        }
    }

    protected virtual async Task<IActionResult> SetSuccessResultAsync(ExtensionGrantContext context, IdentityUser user)
    {
        Logger.LogInformation("Credentials validated for username: {username}", user.UserName);

        var principal = await SignInManager.CreateUserPrincipalAsync(user);

        principal.SetScopes(context.Request.GetScopes());
        principal.SetResources(await GetResourcesAsync(context.Request.GetScopes()));

        await SetClaimsDestinationsAsync(principal);

        await SaveSecurityLogAsync(
            context,
            user,
            OpenIddictSecurityLogActionConsts.LoginSucceeded);

        return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
    }

    protected async virtual Task SaveSecurityLogAsync(
        ExtensionGrantContext context,
        IdentityUser user,
        string action)
    {
        var logContext = new IdentitySecurityLogContext
        {
            Identity = OpenIddictSecurityLogIdentityConsts.OpenIddict,
            Action = action,
            UserName = user.UserName,
            ClientId = await FindClientIdAsync(context)
        };
        logContext.WithProperty("GrantType", Name);

        await IdentitySecurityLogManager.SaveAsync(logContext);
    }

    protected virtual Task<string> FindClientIdAsync(ExtensionGrantContext context)
    {
        return Task.FromResult(context.Request.ClientId);
    }
}
