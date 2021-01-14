using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VaccinateRegistration.Data;

namespace VaccinateRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly VaccinateDbContext context;

        public VaccinationsController(VaccinateDbContext context)
            => this.context = context;

        [HttpPost]
        public async Task<Vaccination> StoreVaccination([FromBody] StoreVaccination vaccination)
            => await context.StoreVaccination(vaccination);
    }
}
