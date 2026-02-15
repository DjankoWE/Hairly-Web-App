using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hairly.Web.ViewModels.Client
{
    public class ClientAppointmentViewModel
    {
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string ServiceName { get; set; } = null!;

        public decimal ServicePrice { get; set; }

        public string Status { get; set; } = null!;
    }
}
