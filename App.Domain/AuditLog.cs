using App.Domain.Helpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class AuditLog : BaseEntity
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? DateTimeCreated { get; } = DateTime.UtcNow;
    }
}
