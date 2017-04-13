using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Offers.ViewModels
{
    public class MyOffersViewModel
    {
        public List<BuyerOffersViewModel> BuyerOffers { get; set; }
    }    

    public class BuyerOffersViewModel
    {
        public string PropertyType { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string StreetName { get; set; }
        public string OfferStatus { get; set; }
        public int OfferId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public int PropertyId { get; set; }
    }
    
}