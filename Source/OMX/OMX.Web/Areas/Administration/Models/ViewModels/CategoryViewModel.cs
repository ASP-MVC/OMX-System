namespace OMX.Web.Areas.Administration.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class CategoryViewModel : IMapFrom<Category>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }
    }
}