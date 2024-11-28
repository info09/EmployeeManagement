﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.BaseLibrary.Entities
{
    public class Employee : BaseEntity
    {
        [Required]
        public string CivilId { get; set; } = string.Empty;
        [Required]
        public string FileNumber { get; set; } = string.Empty;
        [Required]
        public string? JobName { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required, DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; } = string.Empty;
        [Required]
        public string Photo { get; set; } = string.Empty;
        public string? Other { get; set; }

        // Many to One
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }

        public string? TownId { get; set; }
    }
}
