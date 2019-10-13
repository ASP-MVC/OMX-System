namespace OMX.Application.Ads.Commands
{
    using MediatR;
    using OMX.Application.Ads.Queries.GetSubCategoryAds;
    using OMX.Domain;
    using OMX.MVC.Persistence;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreadAdCommandHandler : IRequestHandler<CreadAdCommand, AdModel>
    {
        private OMXDbContext _context;

        public CreadAdCommandHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<AdModel> Handle(CreadAdCommand request, CancellationToken cancellationToken)
        {
            var subCategoryExists = _context.SubCategories.Any(x => x.Id == request.SubCategoryId && !x.IsDeleted);
            if (!subCategoryExists)
            {
                // TODO exception
            }

            var ad = new Ad
            {
                Title = request.Title,
                Content = request.Content,
                Price = request.Price,
                CreatedOn = DateTime.UtcNow,
                Pictures = request.PicturesUrls.Select(x => new Picture { Url = x }).ToList()
            };

            _context.Ads.Add(ad);
            await _context.SaveChangesAsync();
            var adModel = AdModel.Projection.Compile()(ad);
            return adModel;
        }
    }
}
