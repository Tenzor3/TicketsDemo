using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsDemo.Models
{
    public class CreateTicketModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ReservationId { get; set; }
        public bool IncludeTea { get; set; }
        public bool IncludeCoffee { get; set; }
    }
}