using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VaccinateRegistration.Data;

namespace VaccinateRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        public VaccinationsController() { }

        // This class is NOT COMPLETE.
        // Todo: Complete the class according to the requirements

        [HttpPost]
        public Task<Vaccination> StoreVaccination([FromBody] StoreVaccination vaccination)
        {
            throw new NotImplementedException();
        }
    }
}
