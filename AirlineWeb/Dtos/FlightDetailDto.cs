using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Dtos
{
    public class FlightDetailDto
    {
        
        public Guid Id { get; set; }

        public string FlightCode { get; set; }

        public decimal Price { get; set; }
    }
}
