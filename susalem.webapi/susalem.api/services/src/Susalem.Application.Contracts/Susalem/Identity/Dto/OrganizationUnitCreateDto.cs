﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace Susalem.Identity.Dto
{
    public class OrganizationUnitCreateDto : ExtensibleObject
    {
        [Required]
        [DynamicStringLength(typeof(OrganizationUnitConsts), nameof(OrganizationUnitConsts.MaxDisplayNameLength))]
        public string DisplayName { get; set; }

        public Guid? ParentId { get; set; }
    }
}
