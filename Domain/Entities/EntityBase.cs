using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class EntityBase
    {
        public EntityBase() { }

        [Key]
        public int Id { get; set; }

        public DateTime ValidFrom { get; set; } = DateTime.UtcNow;
        public DateTime ValidTo { get; set; } = new DateTime(2090, 1, 1, 0, 0, 0); // DateTime.MaxValue;

        [NotMapped]
        public bool IsExpired => ValidTo < DateTime.Now;
    }
}
