namespace OMX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using OMX.Contracts;
    using OMX.Contracts.Models;

    public class Category : AuditInfo, IDeletableEntity, IEntity
    {
        private ICollection<SubCategory> subCategories;

        public Category()
        {
            this.subCategories = new HashSet<SubCategory>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Title { get; set; }

        [DefaultValue(0)]
        public int Visit { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<SubCategory> SubCategories
        {
            get
            {
                return this.subCategories;
            }
            set
            {
                this.subCategories = value;
            }
        }
    }
}