﻿using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Susalem.Abp.Account
{
    public class SendPhoneResetPasswordCodeDto
    {
        [Required]
        [Phone]
        [DynamicStringLength(typeof(IdentityUserConsts), nameof(IdentityUserConsts.MaxPhoneNumberLength))]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
