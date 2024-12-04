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

        [HttpGet("GetById")]
        public IEnumerable<Physician> GetById()
        {
            return new PhysicianEC().Physicians;                   
        }
    }
}


