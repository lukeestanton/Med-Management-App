using Api.Clinic.Enterprise;
using MedManagementLibrary;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;

        public PhysicianController(ILogger<PhysicianController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Physician> Get()
        {
            return new PhysicianEC().Physicians;
        }

        [HttpGet("{id}")]
        public Physician? GetById(int id)
        {
            return new PhysicianEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public Physician? Delete(int id)
        {
            return new PhysicianEC().Delete(id);
        }

        [HttpPost]
        public Physician? AddOrUpdate([FromBody] Physician? physician)
        {
            return new PhysicianEC().AddOrUpdate(physician);
        }
    }
}

