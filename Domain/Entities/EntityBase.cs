using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    public class EntityBase
    {
        public EntityBase() { }

        [Key]
        public int Id { get; set; }

        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; } 

        [NotMapped]
        public bool IsExpired => ValidTo < DateTime.Now;
    }
}
