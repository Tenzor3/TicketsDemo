using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace TicketsDemo.Data.Entities
{
    [DelimitedRecord(",")]
    public class Place
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int CarriageId { get; set; }
        [FieldHidden]
        public Carriage Carriage { get; set; }
    }
}
