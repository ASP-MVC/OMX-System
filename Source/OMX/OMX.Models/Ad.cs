namespace OMX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using OMX.Contracts;
    using OMX.Contracts.Models;

    public class Ad : AuditInfo, IDeletableEntity, IEntity
    {
        private ICollection<Picture> pictures;

        private ICollection<Comment> comments;

        public Ad()
        {
            this.pictures = new HashSet<Picture>();
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Content { get; set; }

        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int Visit { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }
        
        public virtual User Owner { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<Picture> Pictures
        {
            get
            {
                return this.pictures;
            }
            set
            {
                this.pictures = value;
            }
        }
    }
}