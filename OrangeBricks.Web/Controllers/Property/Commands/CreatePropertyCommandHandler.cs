using OrangeBricks.Web.Models;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class CreatePropertyCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public CreatePropertyCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public async Task Handle(CreatePropertyCommand command)
        {
            var property = new Models.Property
            {
               PropertyType = command.PropertyType,
               StreetName = command.StreetName,
               Description = command.Description,
               NumberOfBedrooms = command.NumberOfBedrooms
            };

            property.SellerUserId = command.SellerUserId;

            _context.Properties.Add(property);

            await _context.SaveChangesToDbAsync();
        }
    }
}