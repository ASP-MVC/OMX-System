namespace OMX.Web.Models.ViewModels
{
    using AutoMapper;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class MinifiedSubCategoryViewModel : IMapFrom<SubCategory>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<SubCategory, MinifiedSubCategoryViewModel>()
                .ForMember(s => s.CategoryName, opt => opt.MapFrom(s => s.Category.Title))
                .ReverseMap();
        }
    }
}