using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VaccinateRegistration.Data;

namespace VaccinateRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly VaccinateDbContext context;

        public RegistrationsController(VaccinateDbContext context)
            => this.context = context;

        // Note: Both of the following implementation options are fine
        [HttpGet]
        //public async Task<GetRegistrationResult?> GetRegistration([FromQuery] long ssn, [FromQuery] int pin)
        //    => await context.GetRegistration(ssn, pin);
        public async Task<IActionResult> GetRegistration([FromQuery] long ssn, [FromQuery] int pin)
        {
            var registration = await context.GetRegistration(ssn, pin);
            if (registration == null)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        [HttpGet]
        [Route("timeSlots")]
        public async Task<IEnumerable<DateTime>> GetTimeslots([FromQuery] DateTime date)
            => await context.GetTimeslots(date);
    }
}
