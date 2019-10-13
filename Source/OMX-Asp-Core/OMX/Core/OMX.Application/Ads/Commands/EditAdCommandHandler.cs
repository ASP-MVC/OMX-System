namespace OMX.Application.Ads.Commands
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.Domain;
    using OMX.MVC.Persistence;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class EditAdCommandHandler : IRequestHandler<EditAdCommand, Unit>
    {
        private OMXDbContext _context;

        public EditAdCommandHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(EditAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (ad == null)
            {
                // TODO exception
            }
            var subCategoryExists = _context.SubCategories.Any(x => x.Id == request.SubCategoryId && !x.IsDeleted);
            if (!subCategoryExists)
            {
                // TODO exception
            }
            
            // TODO partial update?
            ad.Title = request.Title ?? ad.Title;
            ad.Content = request.Content ?? ad.Content;
            ad.Price = request.Price  == default ? ad.Price : request.Price;
            ad.ModifiedOn = DateTime.UtcNow;
            foreach (var p in request.PicturesUrls)
            {
                ad.Pictures.Add(new Picture { Url = p });
            }

            _context.Ads.Update(ad);
            await _context.SaveChangesAsync();
            return Unit.Value;
        }
    }
}