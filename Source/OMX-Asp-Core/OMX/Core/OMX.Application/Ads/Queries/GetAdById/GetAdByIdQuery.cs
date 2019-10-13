namespace OMX.Application.Ads.Queries.GetAdById
{
    using MediatR;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;

    public class GetAdByIdQuery : IRequest<AdModel>
    {
        public int AdId { get; set; }
    }
}
