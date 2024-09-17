using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Abp.Account;

public class GetTwoFactorProvidersInput
{
    [Required]
    public Guid UserId{ get; set; }
}
