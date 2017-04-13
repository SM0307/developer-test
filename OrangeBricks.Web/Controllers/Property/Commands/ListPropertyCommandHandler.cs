using OrangeBricks.Web.Models;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class ListPropertyCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public ListPropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public async Task Handle(ListPropertyCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);
            property.IsListedForSale = true;
            await _context.SaveChangesToDbAsync();
        }
    }
}