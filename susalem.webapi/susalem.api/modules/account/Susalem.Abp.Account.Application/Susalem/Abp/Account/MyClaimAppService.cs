﻿using Susalem.Abp.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Susalem.Abp.Account
{
    [Authorize]
    public class MyClaimAppService : AccountApplicationServiceBase, IMyClaimAppService
    {
        public MyClaimAppService()
        {

        }

        public async virtual Task ChangeAvatarAsync(ChangeAvatarInput input)
        {
            var user = await GetCurrentUserAsync();

            user.Claims.RemoveAll(x => x.ClaimType.Equals(IdentityConsts.ClaimType.Avatar.Name));

            user.AddClaim(GuidGenerator, new Claim(IdentityConsts.ClaimType.Avatar.Name, input.AvatarUrl));

            (await UserManager.UpdateAsync(user)).CheckErrors();

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
