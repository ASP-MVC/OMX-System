namespace OMX.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;
    using OMX.Web.Areas.Administration.Models.ViewModels;

    public class SubCategoryViewModel : IMapFrom<SubCategory>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
        
        public CategoryViewModel Category { get; set; }

        public IEnumerable<AdViewModel> Ads { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubCategory, SubCategoryViewModel>()
                .ForMember(s => s.Category, opt=>opt.MapFrom(s => s.Category))
                .ForMember(s => s.Ads, opt => opt.MapFrom(s => s.Ads))
                .ReverseMap();
        }
    }
}