using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Abp.Identity
{
    public class IdentityUserOrganizationUnitUpdateDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}
