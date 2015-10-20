namespace OMX.Web.Areas.Administration.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class CategoryViewModel : IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [UIHint("String")]
        public string Title { get; set; }

    }
}