﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Susalem.Identity.Dto
{
    public abstract class IdentityUserClaimCreateOrUpdateDto
    {
        [Required]
        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimTypeLength))]
        public string ClaimType { get; set; }

        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimValueLength))]
        public string ClaimValue { get; set; }
    }
}
