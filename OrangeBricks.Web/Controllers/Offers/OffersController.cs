using System.Web.Mvc;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Offers.Builders;
using OrangeBricks.Web.Controllers.Offers.Commands;
using OrangeBricks.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using log4net;
using OrangeBricks.Web.Shared;

namespace OrangeBricks.Web.Controllers.Offers
{
    
    public class OffersController : Controller
    {
        private readonly IOrangeBricksContext _context;
        private readonly ILog _log;
        public OffersController(IOrangeBricksContext context)
        {
            _context = context;

            IOBLogger logger = new OBLogger();
            _log = logger.Getlog4net();
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult OnProperty(int id)
        {
            try
            {
                var builder = new OffersOnPropertyViewModelBuilder(_context);
                var viewModel = builder.Build(id);

                return View(viewModel);
            }
            catch(Exception ex)
            {
                _log.Error($"Error when retriving offers on property.{ex.Message}");
                return View("Error");
            }
            
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public async Task<ActionResult> Accept(AcceptOfferCommand command)
        {
            try
            {
                var handler = new AcceptOfferCommandHandler(_context);

                await handler.Handle(command);

                return RedirectToAction("OnProperty", new { id = command.PropertyId });
            }
            catch (Exception ex)
            {
                _log.Error($"Error when accepting the offer.{ex.Message}");
                return View("Error");
            }            
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public async Task<ActionResult> Reject(RejectOfferCommand command)
        {
            try
            {
                var handler = new RejectOfferCommandHandler(_context);

                await handler.Handle(command);

                return RedirectToAction("OnProperty", new { id = command.PropertyId });
            }
            catch (Exception ex)
            {
                _log.Error($"Error when rejecting the offer.{ex.Message}");
                return View("Error");
            }
            
        }

        [HttpGet]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MyOffers()
        {
            try
            {
                _log.Debug("Retrieveing all the offers placed by the buyer.");
                var builder = new MyOffersViewModelBuilder(_context);
                var viewModel = builder.Build(User.Identity.GetUserId());

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error when retrieving offers placed.{ex.Message}");
                return View("Error");
            }
            
        }
    }
}