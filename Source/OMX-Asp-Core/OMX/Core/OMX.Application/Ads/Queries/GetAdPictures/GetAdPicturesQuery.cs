namespace OMX.Application.Ads.Queries.GetAdPictures
{
    using MediatR;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using System.Collections.Generic;

    public class GetAdPicturesQuery : IRequest<IEnumerable<AdPictureModel>>
    {
        public int AdId { get; set; }
    }
}
