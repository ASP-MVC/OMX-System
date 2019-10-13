namespace OMX.Application.Ads.Queries.GetAdPictures
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using OMX.MVC.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetAdPicturesHandler : IRequestHandler<GetAdPicturesQuery, IEnumerable<AdPictureModel>>
    {
        private OMXDbContext _context;

        public GetAdPicturesHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdPictureModel>> Handle(GetAdPicturesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pictures
               .Where(p => p.AdId == request.AdId && p.IsDeleted == false)
               .Select(AdPictureModel.Projection)
               .ToListAsync();
        }
    }
}
