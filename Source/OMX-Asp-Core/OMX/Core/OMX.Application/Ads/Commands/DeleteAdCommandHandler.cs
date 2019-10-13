namespace OMX.Application.Ads.Commands
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.MVC.Persistence;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, string>
    {
        private OMXDbContext _context;

        public DeleteAdCommandHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var ad = await _context.Ads.FirstOrDefaultAsync(x => x.Id == request.AdId);
            if (ad == null)
            {
                // TODO throw exception
            }

            ad.IsDeleted = true;
            ad.DeletedOn = DateTime.UtcNow;

            _context.Ads.Update(ad);
            await _context.SaveChangesAsync();

            return $"You successfully deleted {ad.Title}";
        }
    }
}
