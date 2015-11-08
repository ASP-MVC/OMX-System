namespace OMX.Web.Models.BindingModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class CreateSubCategoryBindingModel : IMapFrom<SubCategory>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [UIHint("DropDownList")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}