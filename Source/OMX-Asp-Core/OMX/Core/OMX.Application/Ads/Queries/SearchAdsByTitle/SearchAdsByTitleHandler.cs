namespace OMX.Application.Ads.Queries.SearchAdsByTitle
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using OMX.MVC.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SearchAdsByTitleHandler : IRequestHandler<SearchAdsByTitleQuery, IEnumerable<AdModel>>
    {
        private OMXDbContext _context;

        public SearchAdsByTitleHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdModel>> Handle(SearchAdsByTitleQuery request, CancellationToken cancellationToken)
        {
            var q = _context.Ads
                       .Where(a => a.Title.ToLower().Contains(request.AdTitleSubstring.ToLower()))
                       .Select(AdModel.Projection);

            return await q.ToListAsync();
        }
    }
}
