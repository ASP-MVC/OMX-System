namespace OMX.Application.Ads.Commands
{
    using MediatR;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using System;
    using System.Collections.Generic;

    public class CreadAdCommand : IRequest<AdModel>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public int SubCategoryId { get; set; }

        public IEnumerable<string> PicturesUrls { get; set; }
    }
}
