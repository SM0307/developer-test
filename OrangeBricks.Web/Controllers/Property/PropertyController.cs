using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Property.Builders;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;
using System;
using System.Threading.Tasks;
using OrangeBricks.Web.Shared;
using log4net;

namespace OrangeBricks.Web.Controllers.Property
{
    public class PropertyController : Controller
    {
        private readonly IOrangeBricksContext _context;
        private readonly ILog _log;

        public PropertyController(IOrangeBricksContext context)
        {
            _context = context;
            IOBLogger logger = new OBLogger();
            _log = logger.Getlog4net();
        }

        [Authorize]
        public ActionResult Index(PropertiesQuery query)
        {
            try
            {
                var builder = new PropertiesViewModelBuilder(_context);
                var viewModel = builder.Build(query);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error.{ex.Message}");
                return View("Error");
            }
            
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult Create()
        {
            try
            {
                var viewModel = new CreatePropertyViewModel();

                viewModel.PossiblePropertyTypes = new string[] { "House", "Flat", "Bungalow" }
                    .Select(x => new SelectListItem { Value = x, Text = x })
                    .AsEnumerable();

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error.{ex.Message}");
                return View("Error");
            }
            
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        [HttpPost]
        public async Task<ActionResult> Create(CreatePropertyCommand command)
        {
            try
            {
                var handler = new CreatePropertyCommandHandler(_context);

                command.SellerUserId = User.Identity.GetUserId();

                await handler.Handle(command);

                return RedirectToAction("MyProperties");
            }
            catch (Exception ex)
            {
                _log.Error($"Error. {ex.Message}");
                return View("Error");
            }
            
        }

        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult MyProperties()
        {
            try
            {
                var builder = new MyPropertiesViewModelBuilder(_context);
                var viewModel = builder.Build(User.Identity.GetUserId());

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error retrieving the properties.{ex.Message}");
                return View("Error");
            }
            
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Seller")]
        public async Task<ActionResult> ListForSale(ListPropertyCommand command)
        {
            try
            {
                var handler = new ListPropertyCommandHandler(_context);

                await handler.Handle(command);

                return RedirectToAction("MyProperties");
            }
            catch (Exception ex)
            {
                _log.Error($"Error listing the property.{ex.Message}");
                return View("Error");
            }
            
        }

        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult MakeOffer(int id)
        {
            try
            {
                var builder = new MakeOfferViewModelBuilder(_context);
                var viewModel = builder.Build(id);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error making the offer.{ex.Message}");
                return View("Error");
            }
            
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public async Task<ActionResult> MakeOffer(MakeOfferCommand command)
        {
            try
            {
                command.BuyerUserId = User.Identity.GetUserId();
                var handler = new MakeOfferCommandHandler(_context);

                await handler.Handle(command);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _log.Error($"Error making the offer..{ex.Message}");
                return View("Error");
            }
            
        }
        
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public ActionResult BookAppointment(int id)
        {
            try
            {
                var builder = new BookAppointmentViewModelBuilder(_context);
                var viewModel = builder.Build(id);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _log.Error($"Error booking the appointment.{ex.Message}");
                return View("Error");
            }
            
        }

        [HttpPost]
        [OrangeBricksAuthorize(Roles = "Buyer")]
        public async Task<ActionResult> BookAppointment(BookApointmentCommand command)
        {
            try
            {
                command.BuyerUserId = User.Identity.GetUserId();
                var handler = new BookAppointmentCommandHandler(_context);

                await handler.Handle(command);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _log.Error($"Error booking the appoitment.{ex.Message}");
                return View("Error");
            }
            
        }
    }
}