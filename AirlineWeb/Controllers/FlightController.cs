using AirlineWeb.Data;
using AirlineWeb.Dtos;
using AirlineWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirlineWeb.Controllers
{
    [Route("api/flight")]
    public class FlightController : ControllerBase
    {
        RepositoryContext _repo;
        IMapper _mapper;
        public FlightController(RepositoryContext repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet("{flightId}", Name = "GetFlight")]
        public async  Task<IActionResult> GetFlight(Guid flightId)
        {
            try
            {
                var flight = await _repo.FlightDetailS.Where(x => x.Id.Equals(flightId)).SingleOrDefaultAsync();
                if (flight == null)
                {
                    return NotFound("Flight Id Doesnot Exist");
                }
                var flightToReturn = _mapper.Map<FlightDetail>(flight);
                return Ok(flightToReturn);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500);
               
            }
            
        }
    
        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] FlightDetailCreationDto flightToCreate)
        {
                if(flightToCreate == null)
            {
                return BadRequest("The object sent to the create file is null");
            }

            var flightExist = await _repo.FlightDetailS.Where(x => x.FlightCode == flightToCreate.FlightCode).SingleOrDefaultAsync();

            if(flightExist != null)
            {
                return StatusCode(409, "FlightDetail Already Exist");
            }

            var flight = _mapper.Map<FlightDetail>(flightToCreate);

            await _repo.FlightDetailS.AddAsync(flight);
            await _repo.SaveChangesAsync();
            var flightToReturn = _mapper.Map<FlightDetail>(flight);
            return StatusCode(201, "The flight deatil has been created");
        }
    
        [HttpPost("{flightId}")]

        public async Task<IActionResult> UpdateFlight(Guid flightId, [FromBody] FlightDetailForUpdateDto flightToUpdate)
        {
            var flight = await _repo.FlightDetailS.Where(x => x.Id.Equals(flightId)).SingleOrDefaultAsync();
            if (flight == null)
            {
                return NotFound("Flight Id Doesnot Exist");
            }
            var flightToReturn = _mapper.Map<FlightDetail>(flight);

            var oldPrice = flight.Price;
            var newPrice = flightToUpdate.Price;
            if(oldPrice == newPrice)
            {
                return BadRequest("The old price and new price is same");
            }

            var flightUpdate = _mapper.Map<FlightDetail>(flightToUpdate);
            _repo.FlightDetailS.Update(flightUpdate);
            await _repo.SaveChangesAsync();
            return NoContent();
        }

    }
}
