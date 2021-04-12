using System.ComponentModel.DataAnnotations;

namespace App.Domain.Helpers
{
    public class BaseEntity : IBaseEntity
    {
        [Key]
        public int Id { get; set; }

        public int GetId()
        {
            return Id;
        }

    }
}
