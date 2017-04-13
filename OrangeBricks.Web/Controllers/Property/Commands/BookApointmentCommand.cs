using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class BookApointmentCommand
    {
        public int PropertyId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string BuyerUserId { get; set; }
    }
}