using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class MyOffersViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public MyOffersViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public MyOffersViewModel Build(string buyerId)
        {
            var query = _context.Offers.Join(_context.Properties, o => o.Property_Id, p => p.Id, (o, p) => new { Offer = o, Property = p });
            return new MyOffersViewModel
            {
                BuyerOffers = query.Select(x => new BuyerOffersViewModel
                {
                    OfferId = x.Offer.Id,
                    Amount = x.Offer.Amount,
                    CreatedAt = x.Offer.CreatedAt,
                    PropertyId = x.Property.Id,
                    PropertyType = x.Property.PropertyType,
                    StreetName = x.Property.StreetName,
                    NumberOfBedrooms = x.Property.NumberOfBedrooms,
                    Status = x.Offer.Status.ToString()
                }).ToList()
            };            
        }
    }
}