namespace OMX.Application.Ads.Queries.GetAdById
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using OMX.MVC.Persistence;

    public class GetAdByIdHandler : IRequestHandler<GetAdByIdQuery, AdModel>
    {
        private OMXDbContext _context;

        public GetAdByIdHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<AdModel> Handle(GetAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Ads
                .Where(x => x.Id == request.AdId)
                .Select(AdModel.Projection)
                .FirstOrDefaultAsync();
        }
    }
}
