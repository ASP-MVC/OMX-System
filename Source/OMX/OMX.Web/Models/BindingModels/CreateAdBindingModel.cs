namespace OMX.Web.Models.BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class CreateAdBindingModel : IMapFrom<Ad>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [MaxLength(1500)]
        [UIHint("MultiLinetext")]
        public string Content { get; set; }

        [UIHint("Currency")]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedOn { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }

        [DisplayName("Sub Category")]
        [UIHint("DropDownList")]
        public int SubCategoryId { get; set; }

        public IEnumerable<SelectListItem> SubCategories { get; set; }

        public HttpPostedFileBase UploadedImages { get; set; }
    }
}