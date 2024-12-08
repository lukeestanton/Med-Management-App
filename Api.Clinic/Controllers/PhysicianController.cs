using Api.Clinic.Enterprise;
using MedManagementLibrary.DTO;
using MedManagementLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;
        private readonly PhysicianEC _physicianEC;

        public PhysicianController(ILogger<PhysicianController> logger, PhysicianEC physicianEC)
        {
            _logger = logger;
            _physicianEC = physicianEC;
        }

        // GET: /Physician
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicianDTO>>> Get()
        {
            var physicians = await _physicianEC.PhysiciansAsync();
            return Ok(physicians);
        }

        // GET: /Physician/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicianDTO>> GetById(int id)
        {
            var physician = await _physicianEC.GetByIdAsync(id);
            if (physician == null)
            {
                return NotFound();
            }
            return Ok(physician);
        }

        // DELETE: /Physician/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhysicianDTO>> Delete(int id)
        {
            var physician = await _physicianEC.DeleteAsync(id);
            if (physician == null)
            {
                return NotFound();
            }
            return Ok(physician);
        }

        // POST: /Physician/Search
        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<PhysicianDTO>>> Search([FromBody] Query q)
        {
            var physicians = await _physicianEC.SearchAsync(q?.Content ?? string.Empty);
            return Ok(physicians);
        }

        // POST: /Physician
        [HttpPost]
        public async Task<ActionResult<PhysicianDTO>> AddOrUpdate([FromBody] PhysicianDTO? physicianDto)
        {
            if (physicianDto == null)
            {
                return BadRequest("Physician data is null.");
            }

            var physician = await _physicianEC.AddOrUpdateAsync(physicianDto);
            if (physician == null)
            {
                return BadRequest("Failed to add or update physician.");
            }

            var resultDto = new PhysicianDTO
            {
                Id = physician.Id,
                Name = physician.Name,
                Birthday = physician.Birthday
            };

            return CreatedAtAction(nameof(GetById), new { id = physician.Id }, resultDto);
        }
    }
}
