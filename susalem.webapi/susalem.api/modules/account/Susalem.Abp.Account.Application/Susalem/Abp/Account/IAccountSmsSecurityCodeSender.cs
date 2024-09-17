﻿using System.Threading;
using System.Threading.Tasks;

namespace Susalem.Abp.Account
{
    public interface IAccountSmsSecurityCodeSender
    {
        Task SendSmsCodeAsync(
            string phone,
            string token,
            string template, // 传递模板号
            CancellationToken cancellation = default);
    }
}
