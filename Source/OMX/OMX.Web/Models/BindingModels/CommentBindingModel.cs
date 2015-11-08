namespace OMX.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class CommentBindingModel : IMapTo<Comment>
    {
        [StringLength(1500, MinimumLength = 2)]
        public string Content { get; set; }

        public int? AdId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string RecipientId { get; set; }
    }
}