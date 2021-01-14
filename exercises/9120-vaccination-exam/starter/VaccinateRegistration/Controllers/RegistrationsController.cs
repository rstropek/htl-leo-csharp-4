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
        public RegistrationsController() { }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        [HttpGet]
        public Task<GetRegistrationResult?> GetRegistration([FromQuery] long ssn, [FromQuery] int pin)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("timeSlots")]
        public Task<IEnumerable<DateTime>> GetTimeslots([FromQuery] DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
