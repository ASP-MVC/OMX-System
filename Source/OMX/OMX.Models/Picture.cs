namespace OMX.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using OMX.Contracts.Models;

    public class Picture : AuditInfo, IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual User Owner { get; set; }

        public int? AdId { get; set; }

        public virtual Ad Ad { get; set; }
    }
}