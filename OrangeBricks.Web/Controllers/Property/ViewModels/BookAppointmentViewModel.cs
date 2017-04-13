using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrangeBricks.Web.Controllers.Property.ViewModels
{
    public class BookAppointmentViewModel
    {
        public string PropertyType { get; set; }
        public string StreetName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PropertyId { get; set; }
    }
}
