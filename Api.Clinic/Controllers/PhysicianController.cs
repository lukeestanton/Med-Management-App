using Api.Clinic.Database;
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
        public ActionResult<Physician> Create([FromBody] Physician physician)
        {
            var addedPhysician = new PhysicianEC().Add(physician);
            return CreatedAtAction(nameof(GetById), new { id = addedPhysician.ID }, addedPhysician);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Physician physician)
        {
            if (id != physician.ID)
            {
                return BadRequest();
            }

            var updatedPhysician = new PhysicianEC().Update(physician);
            if (updatedPhysician == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}


