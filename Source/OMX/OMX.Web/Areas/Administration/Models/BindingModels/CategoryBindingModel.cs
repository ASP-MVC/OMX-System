namespace OMX.Web.Areas.Administration.Models.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class CategoryBindingModel : IMapTo<Category>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 1)]
        [DisplayName("Category's Title")]
        public string Title { get; set; }
    }
}