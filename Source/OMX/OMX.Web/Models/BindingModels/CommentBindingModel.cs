namespace OMX.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class CommentBindingModel : IMapFrom<Comment>
    {
        [StringLength(1500, MinimumLength = 2)]
        public string Content { get; set; }

        public int? AdId { get; set; }
    }
}