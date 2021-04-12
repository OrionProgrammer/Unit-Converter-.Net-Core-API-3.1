
using App.Domain.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }

        public ICollection<AuditLog> AuditLog { get; set; }

    }
}
