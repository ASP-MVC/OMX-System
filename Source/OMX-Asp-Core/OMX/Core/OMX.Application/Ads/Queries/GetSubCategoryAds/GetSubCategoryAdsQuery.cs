namespace OMX.Application.Ads.Queries.GetSubCategoryAds
{
    using MediatR;
    using System.Collections.Generic;

    public class GetSubCategoryAdsQuery : IRequest<IEnumerable<AdModel>>
    {
        public int SubCategoryId { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; } = 10;
    }
}
