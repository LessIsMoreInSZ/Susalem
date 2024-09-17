﻿using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Susalem.Abp.Account.Emailing;

public interface IAccountEmailConfirmSender
{
    Task SendEmailConfirmLinkAsync(
        IdentityUser user,
        string confirmToken,
        string appName,
        string returnUrl = null,
        string returnUrlHash = null
    );
}
