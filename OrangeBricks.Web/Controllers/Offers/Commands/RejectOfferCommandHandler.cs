using System;
using OrangeBricks.Web.Models;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class RejectOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public RejectOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public async Task Handle(RejectOfferCommand command)
        {
            var offer = _context.Offers.Find(command.OfferId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Rejected;

            await _context.SaveChangesToDbAsync();
        }
    }
}