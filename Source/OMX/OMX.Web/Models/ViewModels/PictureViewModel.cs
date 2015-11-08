namespace OMX.Web.Models.ViewModels
{
    using System;
    using System.Web.Mvc;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class PictureViewModel : IMapFrom<Picture>
    {
        public int Id { get; set; }

        public string Url { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }
    }
}