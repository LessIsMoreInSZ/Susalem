using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Abp.Account;

public class ConfirmEmailInput
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string ConfirmToken { get; set; }
}
