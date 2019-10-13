namespace OMX.Application.Ads.Queries.SearchAdsByTitle
{
    using MediatR;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using System.Collections.Generic;

    public class SearchAdsByTitleQuery : IRequest<IEnumerable<AdModel>>
    {
        public string AdTitleSubstring { get; set; }
    }
}
