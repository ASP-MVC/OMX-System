namespace OMX.Web.Areas.Administration.Models.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class AdminAdViewModel : IMapFrom<Ad>, IMapTo<Ad>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 1)]
        [UIHint("String")]
        public string Title { get; set; }

        [MaxLength(1500)]
        [UIHint("String")]
        public string Content { get; set; }

        [UIHint("Currency")]
        public decimal Price { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Visit { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }

        [UIHint("String")]
        [DisplayName("Owner")]
        public string OwnerUserName { get; set; }

        [UIHint("Integer")]
        public int SubCategoryId { get; set; }

        [UIHint("String")]
        [DisplayName("SubCatTitle")]
        public string SubCategoryName { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Ad, AdminAdViewModel>()
                .ForMember(x => x.SubCategoryName, opt => opt.MapFrom(x => x.SubCategory.Title))
                .ForMember(x => x.OwnerUserName, opt => opt.MapFrom(x => x.Owner.UserName))
                .ReverseMap();
        }
    }
}