using System;
using OrangeBricks.Web.Models;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Offers.Commands
{
    public class AcceptOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public AcceptOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public async Task Handle(AcceptOfferCommand command)
        {
            var offer = _context.Offers.Find(command.OfferId);

            offer.UpdatedAt = DateTime.Now;
            offer.Status = OfferStatus.Accepted;

            await _context.SaveChangesToDbAsync();
        }
    }
}