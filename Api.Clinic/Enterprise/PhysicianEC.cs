using Api.Clinic.Persistence;
using MedManagementLibrary.DTO;
using MedManagementLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Clinic.Enterprise
{
    public class PhysicianEC
    {
        private readonly DatabaseHelper _database;

        public PhysicianEC(DatabaseHelper database)
        {
            _database = database;
        }

        public async Task<IEnumerable<PhysicianDTO>> PhysiciansAsync()
        {
            var physicians = await _database.GetAllPhysiciansAsync();
            return physicians.Select(p => new PhysicianDTO(p));
        }

        public async Task<IEnumerable<PhysicianDTO>> SearchAsync(string query)
        {
            var physicians = await _database.SearchPhysiciansAsync(query);
            return physicians.Select(p => new PhysicianDTO(p));
        }

        public async Task<PhysicianDTO?> GetByIdAsync(int id)
        {
            var physicians = await _database.GetAllPhysiciansAsync();
            var physician = physicians.FirstOrDefault(p => p.Id == id);
            return physician != null ? new PhysicianDTO(physician) : null;
        }

        public async Task<PhysicianDTO?> DeleteAsync(int id)
        {
            var physician = await GetByIdAsync(id);
            if (physician != null)
            {
                await _database.DeletePhysicianAsync(id);
            }
            return physician;
        }

        public async Task<Physician?> AddOrUpdateAsync(PhysicianDTO? physicianDto)
        {
            if (physicianDto == null)
            {
                return null;
            }

            var physician = new Physician
            {
                Id = physicianDto.Id,
                Name = physicianDto.Name,
                Birthday = physicianDto.Birthday
            };

            if (physician.Id <= 0)
            {
                var newId = await _database.AddPhysicianAsync(physician);
                physician.Id = newId;
            }
            else
            {
                await _database.UpdatePhysicianAsync(physician);
            }

            return physician;
        }
    }
}
