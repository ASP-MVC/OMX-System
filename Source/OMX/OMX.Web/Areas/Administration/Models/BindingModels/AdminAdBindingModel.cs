namespace OMX.Web.Areas.Administration.Models.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class AdminAdBindingModel: IMapTo<Ad>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        public string Title { get; set; }

        [MaxLength(1500)]
        public string Content { get; set; }

        public decimal Price { get; set; }

        [DefaultValue(0)]
        public int Visit { get; set; }

        public string OwnerId { get; set; }

        public int SubCategoryId { get; set; }
    }
}