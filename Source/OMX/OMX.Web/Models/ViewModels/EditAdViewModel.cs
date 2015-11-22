namespace OMX.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class EditAdViewModel : IMapFrom<Ad>, IMapTo<Ad>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [UIHint("MultiLinetext")]
        public string Content { get; set; }

        [UIHint("Currency")]
        public decimal Price { get; set; }

        public string OwnerId { get; set; }

        [DisplayName("Sub Category")]
        [UIHint("DropDownList")]
        public int SubCategoryId { get; set; }

        public IEnumerable<SelectListItem> SubCategories { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }

        [UIHint("MultipleFileAsync")]
        public IEnumerable<HttpPostedFileBase> files { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Ad, EditAdViewModel>()
                .ForMember(a => a.Pictures, opt => opt.MapFrom(a => a.Pictures.Where(p => p.IsDeleted == false)))
                .ReverseMap();
        }
    }
}