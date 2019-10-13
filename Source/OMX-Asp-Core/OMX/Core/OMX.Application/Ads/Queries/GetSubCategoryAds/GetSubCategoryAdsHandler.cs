namespace OMX.Application.Ads.Queries.GetSubCategoryAds
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.MVC.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetSubCategoryAdsHandler : IRequestHandler<GetSubCategoryAdsQuery, IEnumerable<AdModel>>
    {
        private OMXDbContext _context;

        public GetSubCategoryAdsHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdModel>> Handle(GetSubCategoryAdsQuery request, CancellationToken cancellationToken)
        {
            var data = _context.Ads
                    .Where(a => a.SubCategoryId == request.SubCategoryId)
                    .OrderByDescending(a => a.CreatedOn)
                    .ThenBy(a => a.Id)
                    .Skip((request.Page - 1 ) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(AdModel.Projection);

            return await data.ToListAsync();
        }
    }
}
