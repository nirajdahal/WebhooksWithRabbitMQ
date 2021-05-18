using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgentWeb.Data;
using TravelAgentWeb.Dtos;

namespace TravelAgentWeb.Controllers
{
    [ApiController]
    [Route("api/notification")]
    public class NotificationsController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public NotificationsController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult FlightChanged([FromBody] FlightDetailUpdateDto flightDetailUpdateDto)
        {
            Console.WriteLine($"Webhook Receieved from: {flightDetailUpdateDto.Publisher}");

            var secretModel = _context.WebHookSecret.FirstOrDefault(s =>
                s.Publisher == flightDetailUpdateDto.Publisher &&
                s.Secret == flightDetailUpdateDto.Secret);

            if (secretModel == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Secret - Ignore Webwook");
                Console.ResetColor();
                return Ok();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Valid Webhook!");
                Console.WriteLine($"Old Price {flightDetailUpdateDto.OldPrice}, New Price {flightDetailUpdateDto.NewPrice}");
                Console.ResetColor();
                return Ok();
            }
        }


    }
}
