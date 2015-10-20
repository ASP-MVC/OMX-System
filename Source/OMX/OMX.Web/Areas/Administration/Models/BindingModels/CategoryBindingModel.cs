namespace OMX.Web.Areas.Administration.Models.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class CategoryBindingModel : IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        [DisplayName("Category's Title")]
        [UIHint("String")]
        public string Title { get; set; }
    }
}