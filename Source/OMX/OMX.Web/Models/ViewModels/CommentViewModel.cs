namespace OMX.Web.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class CommentViewModel : IMapFrom<Comment>
    {
        [StringLength(1500, MinimumLength = 2)]
        public string Content { get; set; }

        public DateTime CommentedAt { get; set; }

        public virtual User Author { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int? AdId { get; set; }

        public string AdTitle { get; set; }
    }
}