namespace OMX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using OMX.Contracts;
    using OMX.Contracts.Models;

    public class SubCategory : AuditInfo, IDeletableEntity, IEntity
    {
        private ICollection<Ad> ads;

        public SubCategory()
        {
            this.ads = new HashSet<Ad>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Ad> Ads
        {
            get
            {
                return this.ads;
            }
            set
            {
                this.ads = value;
            }
        }
    }
}