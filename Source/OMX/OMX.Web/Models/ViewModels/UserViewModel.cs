namespace OMX.Web.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using OMX.Infrastructure.Mapping;
    using OMX.Models;

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [MinLength(2)]
        [UIHint("String")]
        public string UserName { get; set; }

        public string PictureUrl { get; set; }
        
        [Required]
        [UIHint("EmailAddress")]
        public string Email { get; set; }

        [UIHint("Integer")]
        public int PublishedAdsCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(u => u.PublishedAdsCount, opt => opt.MapFrom(u => u.PublishedAds.Count))
                .ReverseMap();
        }
    }
}