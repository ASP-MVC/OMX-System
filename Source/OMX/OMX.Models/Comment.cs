namespace OMX.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using OMX.Contracts;
    using OMX.Contracts.Models;

    public class Comment : AuditInfo, IDeletableEntity, IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1500, MinimumLength = 2)]
        public string Content { get; set; }

        public DateTime CommentedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [DefaultValue(false)]
        public bool IsRead { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        public int? AdId { get; set; }

        public virtual Ad Ad { get; set; }
    }
}