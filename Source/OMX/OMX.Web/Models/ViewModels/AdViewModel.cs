﻿namespace OMX.Web.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using AutoMapper;

    using OMX.Infrastructure.Mappings;
    using OMX.Models;

    public class AdViewModel : IMapFrom<Ad>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [UIHint("MultiLinetext")]
        public string Content { get; set; }

        [UIHint("Currency")]
        public decimal Price { get; set; }

        [UIHint("DateTime")]
        public DateTime CreatedOn { get; set; }

        public int Visit { get; set; }

        [HiddenInput(DisplayValue =  false)]
        public bool IsDeleted { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime? DeletedOn { get; set; }
        
        public UserViewModel Owner { get; set; }

        [UIHint("SingleLineText")]
        public string CategoryName { get; set; }

        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Ad, AdViewModel>()
                .ForMember(a => a.Owner, opt=>opt.MapFrom(a => a.Owner))
                .ForMember(a => a.CategoryName, opt=>opt.MapFrom(a => a.SubCategory.Category.Title))
                .ForMember(a => a.Pictures, opt=>opt.MapFrom(a => a.Pictures))
                .ReverseMap();
        }
    }
}