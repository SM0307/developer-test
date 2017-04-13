using OrangeBricks.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookAppointmentCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public BookAppointmentCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public async Task Handle(BookApointmentCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var appointment = new Appointment
            {
                AppointmentDate = command.AppointmentDate,
                Status = AppointmentStatus.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerUserId = command.BuyerUserId
            };

            if (property.Appointments == null)
            {
                property.Appointments = new List<Appointment>();
            }

            property.Appointments.Add(appointment);

            await _context.SaveChangesToDbAsync();
        }
    }
}
